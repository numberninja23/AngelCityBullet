using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharactersManager : MonoBehaviour
{
    protected static CharactersManager _instance = null;

    public int[] characters = new int[3];
    public int deadNumber = 0;

    private bool invincibility = false;

    public GameObject darwinReticle;

    public GameObject[] charUI = new GameObject[3];
    public Transform[] charUIPos = new Transform[5];


    //Return the instance of this singleton in the scene.    
    public static CharactersManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CharactersManager>();
                if (_instance == null)
                {
                    // still no PlatformerManager present, raise awareness:
                    Debug.LogError("An instance of type CharactersManager is needed in the scene, but there is none!");
                }
            }
            return _instance;
        }
    }

    //Also required for singletons to work.    
    void Awake()
    {
        Cursor.visible = false;

        // only stick around if we are the canonical manager instance:
        if (CharactersManager.Instance != this)
        {
            // we are not actually the same object as the one returned by Instance,
            // so we need to commit suicide :,( :
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

                            //not neccessary for the singleton, but for this specific piece of code.
                            int i;
                            for (i = 0; i < characters.Length; i++)
                            {
                                characters[i] = i;
                            }

        for (i = 0; i < charUI.Length; i++)
        {
     
        }
    }


    private void Update()
    {
        //charUI[0].transform.position = charUIPos[characters[0]].position;
        charUI[0].transform.position = Vector3.MoveTowards(charUI[0].transform.position, charUIPos[characters[0]].position, 25);
        charUI[1].transform.position = Vector3.MoveTowards(charUI[1].transform.position, charUIPos[characters[1]].position, 25);
        charUI[2].transform.position = Vector3.MoveTowards(charUI[2].transform.position, charUIPos[characters[2]].position, 25);

        // press "0" key to reset.
        if (Input.GetKeyDown("0"))
        {
            ResetScene();
        }
        //Game Over event.
        if (deadNumber == 3)
        {
            ResetScene();
        }

        //change the colors of the UI elements to dark.
        for (int i = 0; i <= 2; i++)
        {
            if (characters[i] == 2)
            {
                charUI[i].GetComponent<Image>().color = new Color(1, 1, 1);
            }
            else
            {
                charUI[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            }
        }       

        if(characters[0] == 2)
        {
            darwinReticle.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            darwinReticle.GetComponent<Renderer>().enabled = false;
        }
    }



    //tells the characters what place in the formation they are in.
    public int ThisCharacterPlace(int formationNum)
    {
        return characters[formationNum];
    }

    //tells the characters how long they are invulnerable after being hit by an enemy.
    public bool CheckInvincibility()
    {
        return invincibility;
    }

    //call this to reset scene.
    public void ResetScene()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //tells a character that they can be picked up now after being killed.
    public void SetToPickup(int formationNum)
    {
        characters[formationNum] = 3;
        invincibility = false;
        Debug.Log("Invincibility off");
    }

    public void SwitchDown(int formationNum)
    {
        Debug.Log("Switch down for character #" + formationNum);
        if((characters[formationNum] != 4) && (characters[formationNum] != 3))
        {
            if (characters[formationNum] <= deadNumber)
            {
                characters[formationNum] = 2;
            }
            else
            {
                characters[formationNum] = characters[formationNum] - 1;
            }
        }
    }

    public void SwitchUp(int formationNum)
    {
        Debug.Log("Switch up for character #" + formationNum);
        if ((characters[formationNum] != 4) && (characters[formationNum] != 3))
        {
            if (characters[formationNum] == 2)
            {
                characters[formationNum] = deadNumber;
            }
            else
            {
                characters[formationNum] = characters[formationNum] + 1;
            }
        }
    }

    public void CharacterDie(int formationNum)
    {
        deadNumber = deadNumber + 1;
        Debug.Log("character # " + formationNum + ", welcome to Die");
        SwitchUp(2);
        SwitchUp(1);
        SwitchUp(0);
        characters[formationNum] = 4;
        invincibility = true;
    }

    public void CharacterPickUp(int formationNum)
    {
        deadNumber -= 1;
        characters[formationNum] = deadNumber;
        Debug.Log("character # " + formationNum + ", is back online");
    }  
}