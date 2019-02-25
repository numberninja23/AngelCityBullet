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
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(playerSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(playerSpeed * -1, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, playerSpeed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, playerSpeed * -1, 0);
        }


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
