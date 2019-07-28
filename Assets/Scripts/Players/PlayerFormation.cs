﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFormation : MonoBehaviour
{
    public float playerSpeed;
    public GameObject bluecapsule;
    public Transform blueTarget;
    public GameObject redcapsule;
    public Transform redTarget;
    public GameObject darwinReticle;
    public Transform darwinReticleTarget;

    void Update()
    {     
        //Rotates the player
        var input = new Vector3(Input.GetAxis("Horizontal2") * -1, 0, Input.GetAxis("Vertical2") * -1);

            Vector3 newDir = Vector3.RotateTowards(transform.forward, input, 0.45f, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);


        int layerMask = 1 << 8;
        //Move bluecapsule
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

        //Move darwinRedicle
        if (Physics.Linecast(transform.position, darwinReticle.transform.position, layerMask))
        {
            //print("blocked");
            darwinReticle.transform.position = Vector3.MoveTowards(darwinReticle.transform.position, this.transform.position, 1);
        }
        else if (darwinReticle.transform.position != darwinReticleTarget.position)
        {
            darwinReticle.transform.position = Vector3.MoveTowards(darwinReticle.transform.position, darwinReticleTarget.position, 1);
        }
    }
}