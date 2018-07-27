using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveScript : MonoBehaviour {
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 10.1f*Input.GetAxis("P1_Horizontal") * Time.deltaTime, 0);
        transform.Translate(20*transform.forward * Input.GetAxis("P1_Vertical") * Time.deltaTime);

	}
}
