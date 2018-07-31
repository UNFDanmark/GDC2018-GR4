using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {
    public float rotateSpeed = 3000;
    public float thrust = 10;
    public float airResistance = 1;
    public Rigidbody rb;
    public string inputHorizontalAxis;
    public string inputVerticalAxis;
    public float maxRotationSpeed = 8;
    public float backwardsMoveFactor = 0.7f;
    public float respawnTime;
    public float initialPushOutOfIgloo = 20;
    private float respawnCounter = -1;
    public GameObject respawnAudioSource;
    public GameObject DeathSound;
    public GameObject RocketSound;

    private Vector3 spawnPoint;
    private Quaternion spawnRotation;


    public GameHandlerScript gameHandler;
    public int playerNumber;

    public float killDepth = -5f;
    public float respawnHeight = 1.5f;

    public float bounceMultiplierP1 = 0.3f; //this factor only applies to Player 1, as bouncing is handled only by that player
    public float extraGravityMultiplier = 100f;
    public float baseSpeed = 2.5f;

    public STATE state = STATE.ALIVE;
    public enum STATE
    {
        ALIVE, FALLING, DEAD, RESPAWNING
    }

    private bool extraFall = false;
    ParticleSystem p;

	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
        spawnRotation = transform.rotation;
        p = gameObject.GetComponentInChildren<ParticleSystem>();


        PushOutOfIgloo();
    }

    void OnTriggerEnter(Collider collision)
    {
        print("collision with trigger w tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("Seal"))
        {
            //we've hit a seal!
            Destroy(collision.gameObject);
            gameHandler.AddPoint(playerNumber);
        }

        if (collision.gameObject.CompareTag("KillBox"))
        {
            state = STATE.FALLING;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            extraFall = true;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("collision with object w tag: " + collision.gameObject.tag);

        if (collision.gameObject.tag.Equals("Player2"))
        {
            print("DO THE BOUNCE!");
            Vector3 normal = gameObject.transform.position - collision.gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(collision.gameObject.transform.forward, normal) * bounceMultiplierP1, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(transform.forward, normal) * bounceMultiplierP1, ForceMode.Impulse);
        }
    }
    
    private bool IsIn(float min, float max, float x)
    {
        return x >= min && x <= max;
    }

    // Update is called once per frame
    void Update () {
        if (transform.position.y < killDepth) //player has fallen in the water. it is now dead.
        {
            if (state != STATE.DEAD)
            {
                respawnAudioSource.GetComponent<AudioSource>().PlayDelayed(1f);
                DeathSound.GetComponent<AudioSource>().Play();
            }
            print("You're dead!");
            state = STATE.DEAD;


        }
        else if(transform.position.y < respawnHeight)
        {
            state = STATE.ALIVE;
        }

        if (extraFall)
        {
            rb.AddForce(-rb.velocity.x, -1 * extraGravityMultiplier * (IsIn(-6.6f,18,rb.position.x)?1:0), -rb.velocity.z * (IsIn(-14.9f, 0.4f, rb.position.z) ? 1 : 0), ForceMode.Acceleration);
            print("extra fall being added");
        }


        

        
        if (state==STATE.DEAD) //when the player is dead, do respawn counter
        {
            gameHandler.RespawnTime(playerNumber, respawnCounter - Time.time);

            
            if (respawnCounter == -1)
            {
                respawnCounter = Time.time + respawnTime;
            }
            else if(Time.time>=respawnCounter)
            {
                Respawn();
            }
        }else if(state == STATE.ALIVE)
        {
            //rotate via user input
            if(transform.position.y < 1.5f)
                transform.Rotate(0, rotateSpeed * Input.GetAxis(inputHorizontalAxis) * Time.deltaTime, 0);

        }
    }
    private void Respawn()
    {
        //player is teleported to a spawn point.
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = spawnPoint;
        transform.rotation = spawnRotation;
        respawnCounter = -1;
        extraFall = false;
        rb.

        gameObject.GetComponent<BoxCollider>().enabled = true;
        state = STATE.ALIVE;

        PushOutOfIgloo();
    }

    private void PushOutOfIgloo() {
        rb.AddForce(transform.forward * initialPushOutOfIgloo, ForceMode.VelocityChange);
    }

    bool rocketAudioPlaying = false;
    private void FixedUpdate()
    {
        if(state == STATE.ALIVE) {
            if (transform.position.y < 1.5f) { //prevents the player from moving when inside the igloo
                float moveFactor = Input.GetAxis(inputVerticalAxis) < 0 ? backwardsMoveFactor : 1;
                rb.AddForce(transform.forward * thrust * Input.GetAxis(inputVerticalAxis) * moveFactor, ForceMode.Force);

                //if speed is too small at the beginning, accelerate it to that minimum speed
                if(gameObject.tag.Equals("Player2")) print(rb.velocity.magnitude);

                if(rb.velocity.magnitude < baseSpeed-0.1f && Input.GetAxis(inputVerticalAxis) > 0) 
                {
                    //push that shit
                    rb.velocity = transform.forward.normalized * baseSpeed;
                }

                
                if (Input.GetAxis(inputVerticalAxis) > 0)
                {
                    p.Play();
                    if (!rocketAudioPlaying) { 
                        RocketSound.GetComponent<AudioSource>().Play();
                        rocketAudioPlaying = true;
                    }
                }
                else
                {
                    p.Stop();
                    RocketSound.GetComponent<AudioSource>().Stop();
                    rocketAudioPlaying = false;
                }
            }

        }
        else
        {
            p.Stop();
            RocketSound.GetComponent<AudioSource>().Stop();
            rocketAudioPlaying = false;
        }

    }
}
