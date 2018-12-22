using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject mokey;
    public GameObject char1;
    public GameObject char2;
    public Transform trans1;
    public Transform trans2;
    public Transform trans3;

    public float playerSpeed;


    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(playerSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(playerSpeed *-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, playerSpeed, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, playerSpeed * -1, 0);
        }
       
    }
}
