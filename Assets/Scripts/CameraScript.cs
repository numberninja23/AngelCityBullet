using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    private float step;

    public Transform targetPoint;
    public float speed;

	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(targetPoint.position, transform.position) + speed;
        float step = dist * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, step);
    }
}
