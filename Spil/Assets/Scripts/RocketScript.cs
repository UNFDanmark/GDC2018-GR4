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
	// Use this for initialization
	void Start () {
	}

    void OnCollisionEnter(Collision collision)
    {
        print("collision with object w tag: " + collision.gameObject.tag);
        if (collision.gameObject.tag.Equals("Seal"))
        {
            //we've hit a seal!
            Destroy(collision.gameObject);
        }

        
    }

    // Update is called once per frame
    void Update () {
        //stop automatic rotation
        rb.angularVelocity = new Vector3(0, 0, 0);
        //rotate via user input
        transform.Rotate(0, rotateSpeed * Input.GetAxis(inputHorizontalAxis) * Time.deltaTime, 0);
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * thrust* Input.GetAxis(inputVerticalAxis), ForceMode.Impulse);


        //rb.AddForce(-rb.velocity.normalized * airResistance * rb.velocity.sqrMagnitude, ForceMode.Impulse);

    }
}
