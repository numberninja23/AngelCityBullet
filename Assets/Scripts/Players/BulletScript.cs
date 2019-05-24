using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string bulletType;

    void Start()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        //Get hurt by an enemy.
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
