using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    private float moveDirection;

    public float speed = 0.05f;
    public float hp = 15;
    public float despawnTime;
    public float shootSpeed;

    public GameObject damageParticle;
    public GameObject healthItem;
    public GameObject bulletPrefab;

    public Transform player;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        moveDirection = Random.Range(0, 3);
        StartCoroutine(ChangeDirection());
        StartCoroutine(Fire1());
    }


    void Update()
    {
        //Check what direction to move in and go that way.
        if (moveDirection == 0)
        {
            transform.Translate(-speed, 0, 0);
        }
        if (moveDirection == 1)
        {
            transform.Translate(speed, 0, 0);
        }
        if (moveDirection == 2)
        {
            transform.Translate(0, speed, 0);
        }
        if (moveDirection == 3)
        {
            transform.Translate(0, -speed, 0);
        }


        //Die and drop a health item.
        if (hp <= 0)
        {
            /*
            if (Random.Range(0, 4) == 1)
            {
                var HPItem = (GameObject)Instantiate(
             healthItem,
             this.transform.position,
             this.transform.rotation);
            }
            */
            Destroy(this.gameObject);
        }
    }

    //Get hurt by player bullets.
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("PlayerBullet")) || (other.gameObject.CompareTag("PlayerExplosion")))
        {
            if (other.gameObject.CompareTag("PlayerBullet"))
            {
                Destroy(other.gameObject);
            }
            var hurtParticle = (GameObject)Instantiate(
                         damageParticle,
                         this.transform.position,
                         this.transform.rotation);
            //hurtParticle.transform.parent = this.transform;

            hp = hp - 1;
        }
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerExplosion"))
        {
            var magnitude = 1000;

            var force = player.transform.position - transform.position;
            force.Normalize();
            rb.AddForce(-force * magnitude);
        }
    }


    //Randomly change the direction this enemy is moving in.
    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(0.5f);
        moveDirection = Random.Range(0, 4);
        StartCoroutine(ChangeDirection());
    }

    private void FireGeneral(bool vertHoriz, float direction)
    {
        var bullet = (GameObject)Instantiate(
             bulletPrefab,
             this.transform.position,
             this.transform.rotation);

        // Add velocity to the bullet
        if (vertHoriz == true)
        {
            bullet.GetComponent<Rigidbody>().velocity = this.transform.right * direction;
        }
        else if (vertHoriz == false)
        {
            bullet.GetComponent<Rigidbody>().velocity = this.transform.up * direction;
        }
        // Destroy the bullet after 4 seconds
        Destroy(bullet, 4);
    }

    IEnumerator Fire1()
    {
        yield return new WaitForSeconds(1.5f);
        FireGeneral(true, -shootSpeed);
        FireGeneral(true, shootSpeed);
        FireGeneral(false, -shootSpeed);
        FireGeneral(false, shootSpeed);

        StartCoroutine(Fire1());

    }
}
