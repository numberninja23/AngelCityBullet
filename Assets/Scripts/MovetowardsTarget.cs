using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovetowardsTarget : MonoBehaviour {

    // The target that this character is moving toward in the formation.
    public Transform target;

    // Update is called once per frame
    void Update () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }
}
