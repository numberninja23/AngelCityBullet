using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastEyeScript : MonoBehaviour
{
    //shootWait is how long the BlastEye waits before firing the first round.
    public float shootWait;
    public float shootDelay;
    public float shootDistance;
    public float shootSpeed;

    public int hp;

    private float shootTime;

    public GameObject bulletPrefab;
    public GameObject damageParticle;
    public GameObject player;

    private bool invulnerable = false;

    private Animator anim;

    public Transform bulletSpawn;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        shootTime = shootWait;
        StartCoroutine("Shoot");
    }


    private void Update()
    {
        if((player.transform.position.z > this.transform.position.z) && (invulnerable == false))
        {
            Sleep();
        }

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet") || other.gameObject.CompareTag("AstroBullet"))
        {
            Destroy(other.gameObject);
            if (!invulnerable)
            {
                var hurtParticle = (GameObject)Instantiate(
                        damageParticle,
                        this.transform.position,
                        this.transform.rotation);
                //hurtParticle.transform.parent = this.transform;
                hp = hp - 1;
            }
           
        }
        if (other.gameObject.CompareTag("PlayerExplosion") && (invulnerable == false))
        {
            Sleep();
        }
    }


    void Sleep()
    {
        StopCoroutine("Shoot");
        invulnerable = true;
        Debug.Log("I'm fucking invincible!");
        anim.Play("BlastEyeShrink");
        StartCoroutine("WakeUp");
    }


    private IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(1);

        if (player.transform.position.z < this.transform.position.z)
        {
            anim.Play("BlastEyeRise");
            StartCoroutine("Shoot");
            yield return new WaitForSeconds(1);
            invulnerable = false;
        }
        else
        {
            StartCoroutine("WakeUp");
        }
    }

    private IEnumerator Shoot()
    {
            yield return new WaitForSeconds(shootTime);
            anim.Play("BlastEyeShoot");
            yield return new WaitForSeconds(0.2f);

            var bullet = (GameObject)Instantiate(
                  bulletPrefab,
                  bulletSpawn.position, this.transform.rotation);

            // Add velocity to the bullet this character just created.
            bullet.GetComponent<Rigidbody>().velocity = this.transform.up * -shootSpeed;

            // Destroy that bullet after 1 second.
            Destroy(bullet, shootDistance);
            shootTime = shootDelay;
            StartCoroutine("Shoot");
    }      
}
