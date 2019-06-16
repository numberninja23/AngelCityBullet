using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;



//This little bit here is how we make 2D arrays in unity that are visible in the inspector.
[Serializable]
public class myArray
{
    public Transform[] array;
}


public class PlayerCharacters : MonoBehaviour
{

    // The target that this character is moving toward in the formation.
    [SerializeField] myArray[] target;

    Vector3 lastPos;

    bool isHurt = false;
    bool invincible = false;

    // Speed in units per sec.
    public float speed;
    public float shootSpeed;
    public float health = 10;
    public float despawnTime = 1f;
    public float shootDelayTime = 1f;

    private float blinkTime = 1;

    public int formationSpot;
    public int formationType = 0;
    public int formationNum  = 0;

    public string characterName;
    private string direction = "South";
    private string shooting = "";
    private string walking = "";

    //player01 is what the object compares its rotation to in order to rotate the sprites.
    public GameObject player01;
    public GameObject bulletPrefab;
    public GameObject damageParticle;
    public GameObject healthParticle;
    public GameObject arrow;
    public GameObject muzzleFlare;

    public Transform machPosLeft;
    public Transform machPosRight;
    public Transform resetSpot;
    public Transform bulletSpawn;

    private Rigidbody rb;

    public Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        
    }


    //Tell the character to exit the shooting pose after shooting.
    private IEnumerator ShootStopper()
    {
        yield return new WaitForSeconds(1);
        shooting = "";
    }

    //rapid shoot at a set interval of time.
    private IEnumerator ShootRapid()
    {
        if (health > 0)
        {
            yield return new WaitForSeconds(shootDelayTime);
            Fire();
            StartCoroutine("ShootRapid");
        }
    }


    IEnumerator Blink()
    {
        if (blinkTime >= -1)
        {
            if ((blinkTime <= 0) && (health == 0))
            {
                ResetMe();
            }
            if (GetComponent<Renderer>().enabled == true)
            {
                GetComponent<Renderer>().enabled = false;
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
            }
            yield return new WaitForSeconds(0.1f);
            blinkTime -= 0.1f;
            StartCoroutine("Blink");
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
    }

    /*
    IEnumerator StopBeingHurt()
    {
        yield return new WaitForSeconds(0.5F);
        isHurt = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    */


    void Update()
            {
                //Check how much HP the guy has and if they're dead, make them act dead.
                if (health <= 0)
                {
                    health = 0;
                    direction = "Dead";
                    walking = "";
                    shooting = "";
                }

                // The step size is equal to speed times frame time.

                // Sets the sprite/animation of the character to what it should be according to all the neccessary variables.


                    if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName(characterName + direction + walking + shooting))
                    {
                        anim.Play(characterName + direction + walking + shooting);
                    }
                    else
                    {
                        // Checks the direction the character is facing and sets what direction the sprites need to face respectively.

                        if (player01.transform.rotation.eulerAngles.y > 22.5 && player01.transform.rotation.eulerAngles.y < 67.5)
                        {
                            direction = "SouthWest";
                        }

                        if (player01.transform.rotation.eulerAngles.y > 67.5 && player01.transform.rotation.eulerAngles.y < 112.5)
                        {
                            direction = "West";
                        }

                        if (player01.transform.rotation.eulerAngles.y > 112.5 && player01.transform.rotation.eulerAngles.y < 157.5)
                        {
                            direction = "NorthWest";
                        }

                        if (player01.transform.rotation.eulerAngles.y > 157.5 && player01.transform.rotation.eulerAngles.y < 202.5)
                        {
                            direction = "North";
                        }

                        if (player01.transform.rotation.eulerAngles.y > 202.5 && player01.transform.rotation.eulerAngles.y < 247.5)
                        {
                            direction = "NorthEast";
                        }

                        if (player01.transform.rotation.eulerAngles.y > 247.5 && player01.transform.rotation.eulerAngles.y < 292.5)
                        {
                            direction = "East";
                        }

                        if (player01.transform.rotation.eulerAngles.y > 292.5 && player01.transform.rotation.eulerAngles.y < 337.5)
                        {
                            direction = "SouthEast";
                        }

                        if (this.transform.rotation.eulerAngles.y > 337.5 && player01.transform.rotation.eulerAngles.y < 360 || player01.transform.rotation.eulerAngles.y > 0 && player01.transform.rotation.eulerAngles.y < 22.5 || player01.transform.rotation.eulerAngles.y == 0)
                        {
                            direction = "South";
                        }

                        Vector3 curPos = this.transform.position;
                        if (curPos != lastPos)
                        {
                            walking = "W";
                        }
                        else
                        {
                            walking = "";
                            //print("no walk");
                        }
                        lastPos = curPos;
                    }

                if (isHurt == false)
                {
                    //cycle through character positions
                    if (Input.GetButtonDown("Switch Up"))
                    {
                        CharactersManager.Instance.SwitchUp(formationNum);
                    }

                    if (Input.GetButtonDown("Switch Down"))
                    {
                        CharactersManager.Instance.SwitchDown(formationNum);
                    }

                    //Fire this character's gun
                    if ((Input.GetAxis("Trigger") > 0) && (shooting != "S") && (health > 0))
                            {
                                shooting = "S";
                                Fire();
                                StartCoroutine("ShootRapid");

                            }
                    if ((Input.GetAxis("Trigger") <= 0))
                    {
                        StopCoroutine("ShootRapid");
                        muzzleFlare.SetActive(false);
                    }

                    // Check the rotation of the character object/ other various testing things.
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        print(this.transform.rotation.eulerAngles.z);
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

                    //Moves the character to their position in the character formation.
                    transform.position = Vector3.MoveTowards(transform.position, target[formationType].array[formationSpot].position, 0.5f);
                    arrow.transform.rotation = Quaternion.Euler(90, 0, player01.transform.eulerAngles.y * -1);
        }


        formationSpot = CharactersManager.Instance.ThisCharacterPlace(formationNum);

        invincible = CharactersManager.Instance.CheckInvincibility();
    }


    void Fire()
    {
        StopCoroutine("ShootStopper");

        if ((health > 0)  && (formationSpot == 2))
        {
            // Create the Bullet from the Bullet Prefab.
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                bulletSpawn.position, this.transform.rotation);

            if (this.characterName == "Maria")
            {
                muzzleFlare.SetActive(true);
                bullet.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, player01.transform.eulerAngles.y + 90, this.transform.eulerAngles.z);
            }

            // Add velocity to the bullet this character just created.
            bullet.GetComponent<Rigidbody>().velocity = player01.transform.forward * shootSpeed;

            // Destroy that bullet after 1 second.
            Destroy(bullet, despawnTime);
        }
        else
        {
            muzzleFlare.SetActive(false);
        }
        StartCoroutine("ShootStopper");       
    }


    void ResetMe()
    {
        CharactersManager.Instance.SetToPickup(formationNum);
        this.transform.position = resetSpot.position;
        health = 1;
        if (direction == "Dead")
        {
            direction = "South";
        }
    }


    void OnTriggerEnter(Collider other)
    {
        //This is for passing through some collision types. ((((UNFINISHED. GET TO WORK ON THIS LATER.))))
        if (other.gameObject.tag == "theObjectToIgnore")
        {
                Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
        }


        if ((other.gameObject.tag == "Player") && (formationSpot == 3))
        {
            isHurt = false;
            formationSpot = 0;
            CharactersManager.Instance.CharacterPickUp(formationNum);
        }


        if (other.gameObject.CompareTag("HPItem"))
        {
            Destroy(other.gameObject);
            if(health <= 3)
            {
            health = health + 1;
            }
            var healParticle = (GameObject)Instantiate(
               healthParticle,
               this.transform.position,
               this.transform.rotation);
            healParticle.transform.parent = this.transform;
        }        
    }

    void OnTriggerStay(Collider other)
    {
        //Get hurt by an enemy.
        if (other.gameObject.CompareTag("Enemy") && (formationSpot == 2) && (invincible == false) && (isHurt == false))
        {
            /*
                //stop moving this character.
                isHurt = true;

                health = health - 1;
                var hurtParticle = (GameObject)Instantiate(
                   damageParticle,
                   this.transform.position,
                   this.transform.rotation);
                  hurtParticle.transform.parent = this.transform;

                //knockback
                var magnitude = 100;

                var force = transform.position - other.transform.position;
                force.Normalize();
                rb.AddForce(-force * magnitude);
                StartCoroutine(StopBeingHurt());
            */
            isHurt = true;
            CharactersManager.Instance.CharacterDie(formationNum);
            health = 0;
            blinkTime = 1;
            StartCoroutine("Blink");
        }

        if (other.gameObject.CompareTag("HPPad"))
        {
            health = 3;
            var healParticle = (GameObject)Instantiate(
               healthParticle,
               this.transform.position,
               this.transform.rotation);
            healParticle.transform.parent = this.transform;
        }
    }
}

