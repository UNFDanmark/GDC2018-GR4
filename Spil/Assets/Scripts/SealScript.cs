using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealScript : MonoBehaviour {
    public Rigidbody rb;
    public Vector3 direction;

    public Vector3
        l1 = new Vector3(-15, 0.5f, 6);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rb.velocity = direction;	
	}
}
