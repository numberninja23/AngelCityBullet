using System;
using System.Collections;
using System.Collections.Generic;
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
    
        // Speed in units per sec.
        public float speed;
        public float health = 10;

        public int formationSpot = 0;
        public int formationType = 0;

        private string direction = "South";
        private string characterName = "Astrolad";
        private string shooting = "";
        private string walking = "";

        public Animator anim;

        public Text HPText;

    //((((REMOVE THIS VARIABLE AFTER YOU ADD A GOOD UI))))
    public string characterNumber;



    //player01 is what the object compares its rotation to in order to rotate the sprites.
    public GameObject player01;
        public GameObject bulletPrefab;
        public GameObject damageParticle;



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
                yield return new WaitForSeconds(0.2f);
                Fire();
                StartCoroutine("ShootRapid");
            }
        }


        void Update()
        {
        HPText.text = characterNumber + "HP: " + health;

        // The step size is equal to speed times frame time.
        float dist = Vector3.Distance(target[formationType].array[formationSpot].position, transform.position);
            float step = dist * 7 * Time.deltaTime;

            // Sets the sprite/animation of the character to what it should be according to all the neccessary variables.

            if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName(characterName + direction + walking + shooting))
            {
                anim.Play(characterName + direction + walking + shooting);
            }

            //Check how much HP the guy has and if they're dead, make them act dead.
            if (health <= 0)
            {
                health = 0;
                direction = "Dead";
                walking = "";
                shooting = "";
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

                if (this.transform.rotation.eulerAngles.y > 337.5 && player01.transform.rotation.eulerAngles.y < 360 || player01.transform.rotation.eulerAngles.y > 0 && player01.transform.rotation.eulerAngles.y < 22.5)
                {
                    direction = "South";
                }

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L))
                {
                    walking = "W";
                }
                else
                {
                    walking = "";
                }

                if (Input.GetKeyDown(KeyCode.K) && (health > 0))
                {
                    shooting = "S";
                    StartCoroutine("ShootRapid");
                    Fire();
                }
                if (Input.GetKeyUp(KeyCode.K))
                {
                    StopCoroutine("ShootRapid");
                }
            }

            // Check the rotation of the character object/ other various testing things.
            if (Input.GetKeyDown(KeyCode.P))
            {
                print(this.transform.rotation.eulerAngles.z);
            }

            //cycle through character positions
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (formationSpot == 0)
                {
                    formationSpot = 2;
                }
                else
                {
                    formationSpot -= 1;
                }
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
          
            // Move our position a step closer to the target, which is the point the character wants to be at within the formation.
            transform.position = Vector3.MoveTowards(transform.position, target[formationType].array[formationSpot].position, step);
        }



        void Fire()
        {
            StopCoroutine("ShootStopper");
            // Create the Bullet from the Bullet Prefab.
            var bullet = (GameObject)Instantiate(
                bulletPrefab,
                this.transform.position,
                this.transform.rotation);

            // Add velocity to the bullet this character just created.
            bullet.GetComponent<Rigidbody>().velocity = player01.transform.up * -15;

            // Destroy that bullet after 1 second.
            Destroy(bullet, 0.5f);
            StartCoroutine("ShootStopper");
        }



        void OnTriggerEnter(Collider other)
        {
        //Get hurt by an enemy.
            if (other.gameObject.CompareTag("Enemy"))
            {
                health = health - 1;
            var hurtParticle = (GameObject)Instantiate(
               damageParticle,
               this.transform.position,
               this.transform.rotation);
        }
            //This is for passing through some collision types. ((((UNFINISHED. GET TO WORK ON THIS LATER.))))
            if (other.gameObject.tag == "theObjectToIgnore")
            {
                Physics.IgnoreCollision(other.GetComponent<Collider>(), GetComponent<Collider>());
            }
            if (other.gameObject.CompareTag("HPItem"))
            {
                Destroy(other.gameObject);
                health = health + 1;
            }
    }
    }

