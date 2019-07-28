using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlobScript : MonoBehaviour
{
    private bool moving = false;

    Animator m_Animator;

    public GameObject player;
    public GameObject blobPrefab;
    public GameObject mySprite;

    public Transform blobSpawn;
    public Transform blobSpawn2;

    public float size;
    private float damage = 0;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = mySprite.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //StartCoroutine("HealMe");
    }

    // Update is called once per frame
    void Update()
    {
        mySprite.transform.position = new Vector3 (mySprite.transform.position.x, -0.5f, mySprite.transform.position.z);

        if ((Vector3.Distance(this.transform.position, player.transform.position) < 15) && !moving)
        {
            StartCoroutine("StartMoving");
            m_Animator.Play("BlobSpawn");
        }


        if (moving == true)
        {
            float speed;
            if (size < 1)
            {
                speed = 1 - (damage / 8);
            }
            else
            {
                speed = 1 + size - (damage / 8);
            }

            if(size >= 1.8f)
            {
                size = 1.8f;
            }

            if (speed <= 0)
            {
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0);
                agent.destination = player.transform.position;
                agent.speed = 0;
            }
            else
            {
                //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
                agent.destination = player.transform.position;
                agent.speed = speed;
            }

            if (size < 0.42)
            {
                m_Animator.speed = 4.5f - (10 * size);
            }
            else
            {
                m_Animator.speed = 4.5f - (10 * 0.42f);
            }

            transform.localScale = new Vector3((6.1f + (20 * size)), (6.1f + (20 * size)), 1);
            mySprite.transform.localScale = new Vector3(1, (6.1f + (20 * size)), 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") && moving)
        {
            Destroy(other.gameObject);
            size += 0.03f;
        }
        if (other.gameObject.CompareTag("Enemy") && moving)
        {
            Destroy(other.gameObject);
            size += 0.1f;
            Debug.Log(size);
        }       
        if (other.gameObject.CompareTag("AstroBullet"))
        {
            Destroy(other.gameObject);
            if(damage <= 1)
            {
                mySprite.gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0.02f, 0.02f, 0);
                damage += 0.02f;
            }
            else
            {
                damage = 1;
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0);
            }
        }
        if (other.gameObject.CompareTag("PlayerExplosion") && damage > 0.8)
        {
           // if (size < 0.07)
           // {
                Destroy(this.gameObject);
           // }
           /*
            else
            {
                var spawn = (GameObject)Instantiate(
                   blobPrefab,
                   blobSpawn.position, this.transform.rotation);
                var spawn2 = (GameObject)Instantiate(
                  blobPrefab,
                  blobSpawn2.position, this.transform.rotation);

                Destroy(this.gameObject);
            }
            */
        }
    }

    private IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(1);
        moving = true;
    }

    private IEnumerator HealMe()
    {
        yield return new WaitForSeconds(4);
        if(damage >= 0)
        {
            damage -= 0.2f;
            this.gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0.02f, 0.02f, 0);
            Debug.Log(this.gameObject.GetComponent<SpriteRenderer>().color);
        }      
        StartCoroutine("HealMe");
    }
}
