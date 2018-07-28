using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealScript : MonoBehaviour {
    public Rigidbody rb;

    public float sealSpeed = 3;

    public float maxIceDistance = 10;

    public STATE state = STATE.LANDFALL;

    public enum STATE
    {
        LANDFALL, ON_LAND, DIVING
    }
	
	// Update is called once per frame
	void Update () {

        if(state == STATE.LANDFALL)
        {
            //seal is going on land after spawning in water. 
            rb.velocity = new Vector3(0, sealSpeed, 0) + transform.forward * sealSpeed;
            if(transform.position.y >= 0.5f) {
                state = STATE.ON_LAND;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
            }
        }
        else if (state == STATE.ON_LAND)
        {
            //seal is on land. it moves straight until it reaches end of ice shelf.
            rb.velocity = new Vector3(0, rb.velocity.y, 0) + transform.forward * sealSpeed;
            if (!IsOnIce())
            {
                //seal leaves ice
                state = STATE.DIVING;
                rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            }
        }
        else
        {
            //seal is diving. when it's submerged, destroy it.
            if (transform.position.y < -3)
            {
                Destroy(gameObject);
            }
        }
    }

    private bool IsOnIce()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, maxIceDistance);

        foreach (Collider nearbyCollider in nearbyColliders)
        {
            if (nearbyCollider.CompareTag("Ice"))
            {
                return true;
            }
        }
        return false;
    }
}
