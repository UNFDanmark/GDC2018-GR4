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
    public float respawnTime;
    private float respawnCounter = -1;

    private Vector3 spawnPoint;
    private Quaternion spawnRotation;


    public GameHandlerScript gameHandler;
    public int playerNumber;

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
        
    }

    // Update is called once per frame
    void Update () {
        //rotate via user input
        transform.Rotate(0, rotateSpeed * Input.GetAxis(inputHorizontalAxis) * Time.deltaTime, 0);

        if (transform.position.y < -5)
        {
            
            
            if (respawnCounter == -1)
            {
                respawnCounter = Time.time + respawnTime;
            }
            else if(Time.time>=respawnCounter)
            {
                Respawn();
            }
        }
    }
    private void Respawn()
    {
        //player has fallen in the water. it is now dead.
        //player is teleported to a spawn point.
        transform.position = spawnPoint;
        rb.velocity = new Vector3(0, 0, 0);
        transform.rotation = spawnRotation;
        respawnCounter = -1;
    }
    
    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * thrust* Input.GetAxis(inputVerticalAxis), ForceMode.Force);


        //rb.AddForce(-rb.velocity.normalized * airResistance * rb.velocity.sqrMagnitude, ForceMode.Impulse);

    }
}
