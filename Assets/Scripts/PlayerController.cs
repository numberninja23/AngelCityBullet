using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    //test camera types:
    public int camTypeNumber = 0;
    public GameObject[] camType;

    public float playerSpeed;


    // Update is called once per frame
    void Update()
    {
        //Move character
        transform.Translate(playerSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime);


        //Switch between the cameras being used.
        if (Input.GetKeyDown(KeyCode.P))
        {
            camType[camTypeNumber].SetActive(false);
            if (camTypeNumber == 1)
            {
                camTypeNumber = 0;
            }
            else
            {
                camTypeNumber = 1;
            }
            camType[camTypeNumber].SetActive(true);
        }
    }
}
