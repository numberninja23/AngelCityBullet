using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    private float moveDirection;
    public float speed = 0.05f;
    public float hp = 10;

    public GameObject damageParticle;
    public GameObject healthItem;
    public Transform movement;
    private Vector3 moveAngle;


    void Start()
    {
        //Picks a random angle from 30 to 90 and sets its direction to that.
        
        moveDirection = Random.Range(30, 90);
        //moveAngle = new Vector3(moveAngle.x, moveDirection, moveAngle.z);
        movement.Rotate(0, 0, moveDirection);
        StartCoroutine(ChangeDirection());
    }


    void Update()
    {
        //Check what direction to move in and go that way.

        /*
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
        */

        //Move according to ChangeDirection().
        movement.Translate(speed, 0, 0);
        Vector3 temp = movement.position;
        movement.position = transform.position;
        transform.position = temp;
          

        //Die and drop a health item.
        if (hp <= 0)
        {
            var HPItem = (GameObject)Instantiate(
             healthItem,
             this.transform.position,
             this.transform.rotation);

            Destroy(this.gameObject);
        }
    }

    //Get hurt by player bullets.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            var hurtParticle = (GameObject)Instantiate(
               damageParticle,
               this.transform.position,
               this.transform.rotation);
            hp = hp - 1;
        }
    }

    //Randomly change the direction this enemy is moving in (from 30 to 90 degrees).
    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(2f);
        moveDirection = Random.Range(30, 90);
        //moveAngle = new Vector3(moveAngle.x, moveDirection, moveAngle.z);
        movement.Rotate(0, 0, moveDirection);
        StartCoroutine(ChangeDirection());
    }
}
