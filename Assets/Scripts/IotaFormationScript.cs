using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IotaFormationScript : MonoBehaviour {
    public GameObject spot1;
    public GameObject spot2;
    public GameObject spot3;
    Vector3 distanceSpot;
    public float overallRadius;

    Vector3 char1ToChar2;
    Vector3 char2ToChar1;


    //Variables to test distance
    public float hitPoints = 100.0F;
    public Collider coll;
    void Start()
    {
        coll = GetComponent<Collider>();
    }



    private float ApplyHitPoints(Vector3 collisionPos, float radius)
    {
        // The distance from the explosion position to the surface of the collider.
        Vector3 closestPoint = coll.ClosestPointOnBounds(collisionPos);
        float distance = Vector3.Distance(closestPoint, collisionPos);

        return distance;
    }



	void Update () {
        //distanceSpot = new Vector3(spot2.transform.position.x, spot2.transform.position.y, spot2.transform.position.z);
        //spot1.transform.IsChildOf(spot2.transform);

        //Quaternion rotationDelta = Quaternion.FromToRotation(spot2.transform.forward, spot1.transform.forward);

        //spot2.transform.localRotation = Quaternion.identity;

        //spot1.transform.position = new Vector3(distanceSpot.x - overallRadius, distanceSpot.y, distanceSpot.z);

        Vector3 char1ToChar2 = spot2.transform.position - spot1.transform.position;
        Vector3 char2ToChar1 = spot1.transform.position - spot2.transform.position;

        char1ToChar2 = char1ToChar2.normalized;

        float relativeAngle = Vector3.Angle(spot1.transform.forward, char1ToChar2);

        Vector3 localRelativePosition = spot1.transform.InverseTransformDirection(char1ToChar2);
    }
}

