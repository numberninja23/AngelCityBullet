using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string bulletType;

    public GameObject explosion;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (bulletType == "bomb")
        {
            StartCoroutine("DestroySelf");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Hit a wall
        if ((other.gameObject.CompareTag("Wall")) && (bulletType != "bomb") && (bulletType != "Darwin"))
        {
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall") && ((bulletType == "bomb") || (bulletType == "Darwin")))
        {
            rb.velocity = Vector3.zero;
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        if (bulletType == "Darwin")
        {
            var bullet = (GameObject)Instantiate(
                explosion,
                this.transform.position, this.transform.rotation);
        }
    }
}
