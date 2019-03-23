using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandRotate : MonoBehaviour {

	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            this.transform.Rotate(0, 0, 5);
        }
        if (Input.GetKey(KeyCode.L))
        {
            this.transform.Rotate(0, 0, -5);
        }
    }
}
