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
    private float respawnCounter = -1;

    private Vector3 spawnPoint;
    private Quaternion spawnRotation;


    public GameHandlerScript gameHandler;
    public int playerNumber;

    public float killDepth = -5f;
    public float respawnHeight = 1.5f;

    public STATE state = STATE.ALIVE;
    public enum STATE
    {
        ALIVE, DEAD, RESPAWNING
    }

	// Use this for initialization
	void Start () {
        spawnPoint = transform.position;
        spawnRotation = transform.rotation;
	}

    void OnTriggerEnter(Collider collision)
    {
        print("collision with object w tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("Seal"))
        {
            //we've hit a seal!
            Destroy(collision.gameObject);
            gameHandler.AddPoint(playerNumber);
        }

        if (collision.gameObject.CompareTag("KillBox"))
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update () {
        if (transform.position.y < killDepth) //player has fallen in the water. it is now dead.
        {
            state = STATE.DEAD;
        }else if(transform.position.y < respawnHeight)
        {
            state = STATE.ALIVE;
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
            transform.Rotate(0, rotateSpeed * Input.GetAxis(inputHorizontalAxis) * Time.deltaTime, 0);

        }
    }
    private void Respawn()
    {
        //player is teleported to a spawn point.
        transform.position = spawnPoint;
        rb.velocity = new Vector3(0, 0, 0);
        transform.rotation = spawnRotation;
        respawnCounter = -1;

        gameObject.GetComponent<MeshCollider>().enabled = true;
        state = STATE.ALIVE;
    }

    public void OnCollisionEnter(Collision collision)
    {
       
    }

    private void FixedUpdate()
    {
        if(state == STATE.ALIVE) { 
            float moveFactor = Input.GetAxis(inputVerticalAxis) < 0 ? backwardsMoveFactor : 1;
            rb.AddForce(transform.forward * thrust* Input.GetAxis(inputVerticalAxis) * moveFactor, ForceMode.Force);

            ParticleSystem p = gameObject.GetComponentInChildren<ParticleSystem>();

            if(Input.GetAxis(inputVerticalAxis) > 0)
            {
                p.Play();
            }
            else
            {
                p.Stop();
            }

        }

    }
}
