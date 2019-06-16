using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    //test camera types:
    public int camTypeNumber = 0;
    public GameObject[] camType;
    public Rigidbody rb;
    public float playerSpeed = 15;


    // Update is called once per frame
    void Update()
    {
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

        if ((Input.GetAxis("Trigger") > 0))
        {
            playerSpeed = 11;
        }
        else
        {
            playerSpeed = 15;
        }
    }
}
