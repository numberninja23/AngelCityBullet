using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovetowardsTarget : MonoBehaviour {

    // The target that this character is moving toward in the formation.
    [SerializeField] myArray[] target;

    public int formationSpot = 0;
    public int formationType = 0;

    public NavMeshAgent agent;




    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (formationSpot == 0)
            {
                formationSpot = 2;
            }
            else
            {
                formationSpot -= 1;
            }
        }

        //Change the formation type.
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (formationType == 0)
            {
                formationType = 2;
            }
            else
            {
                formationType -= 1;
            }
        }
        // Move our position a step closer to the target, which is the point the character wants to be at within the formation.
        //transform.position = Vector3.MoveTowards(transform.position, target[formationType].array[formationSpot].position, step);
        agent.SetDestination(target[formationType].array[formationSpot].position);
    }
}
