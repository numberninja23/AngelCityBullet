using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float moveDirection;
    public float speed = 0.05f;
    public float hp = 10;

    public GameObject damageParticle;
    public GameObject healthItem;


    // Use this for initialization
    void Start()
    {
        moveDirection = Random.Range(0, 3);
        StartCoroutine(Begin());
    }

    // Update is called once per frame
    void Update()
    {
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

        if(hp <= 0)
        {
            var HPItem = (GameObject)Instantiate(
             healthItem,
             this.transform.position,
             this.transform.rotation);

            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
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

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(0.5f);
        moveDirection = Random.Range(0, 4);
        StartCoroutine(Begin());
    }
}
