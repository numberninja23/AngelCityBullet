using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string bulletType;

    public GameObject explosion;


    void Start()
    {
        if(bulletType == "bomb")
        {
            StartCoroutine("DestroySelf");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Hit a wall
        if ((other.gameObject.CompareTag("Wall")) && (bulletType != "bomb"))
        {
            Destroy(this.gameObject);
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
