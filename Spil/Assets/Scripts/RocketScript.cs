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
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, rotateSpeed * Input.GetAxis(inputHorizontalAxis) * Time.deltaTime, 0);
	}

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * thrust* Input.GetAxis(inputVerticalAxis), ForceMode.Impulse);


        rb.AddForce(-rb.velocity.normalized * airResistance * rb.velocity.sqrMagnitude, ForceMode.Impulse);

    }
}
