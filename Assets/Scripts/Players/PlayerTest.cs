using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float playerSpeed;
    public GameObject bluecapsule;
    public Transform blueTarget;
    public GameObject redcapsule;
    public Transform redTarget;

    void Update()
    {
        /*
        //Rotates the player
        if (Input.GetKey(KeyCode.J))
        {
            transform.Rotate(Vector3.down, 200 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(Vector3.up, 200 * Time.deltaTime);
        }
        */


        //Rotates the player ver. 2
        var input = new Vector3(Input.GetAxis("Horizontal2") * -1, 0, Input.GetAxis("Vertical2") * -1);
        if (input != Vector3.zero)
        {
            transform.forward = input;
        }



        //Move bluecapsule
        int layerMask = 1 << 8;
        if (Physics.Linecast(transform.position, bluecapsule.transform.position, layerMask))
        {
                //print("blocked");
                bluecapsule.transform.position = Vector3.MoveTowards(bluecapsule.transform.position, this.transform.position, 0.2f);
        }
        else if (bluecapsule.transform.position != blueTarget.position)
        {
            bluecapsule.transform.position = Vector3.MoveTowards(bluecapsule.transform.position, blueTarget.position, 0.2f);
        }

        //Move redcapsule
        if (Physics.Linecast(transform.position, redcapsule.transform.position, layerMask))
        {
            //print("blocked");
            redcapsule.transform.position = Vector3.MoveTowards(redcapsule.transform.position, this.transform.position, 0.2f);
        }
        else if (redcapsule.transform.position != redTarget.position)
        {
            redcapsule.transform.position = Vector3.MoveTowards(redcapsule.transform.position, redTarget.position, 0.2f);
        }
    }
}