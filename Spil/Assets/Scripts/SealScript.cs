using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealScript : MonoBehaviour {
    public Rigidbody rb;

    public float sealSpeed = 3;

    public float maxIceDistance = 10;

    public Vector3 target;
    private Vector3 dir;
    private Vector3 dirNorm;

    public STATE state = STATE.LANDFALL;

    public enum STATE
    {
        LANDFALL, ON_LAND, DIVING
    }

    private float startTurnY = 0.3f;
    private float stopTurnY = 0.5f;
    private float turnConst = 1.7f;
    private float turnVal = 0;
    public float turnIncrement = 30f;

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        dir = (target - transform.position);
    }

    private bool beenOnLand = false;

	// Update is called once per frame
	void Update () {
        if (state == STATE.LANDFALL)
        {
            //moves upwards until it reaches a certain height.
            //TODO remember NO GRAVITY
            
            //Quaternion rotation = Quaternion.LookRotation(relativePos);
            //transform.rotation = rotation;

            

            
            //if it's close to the edge of the screen, turn on gravity so it can fall.
            if (!IsOnIce() && beenOnLand)
            {
                turnVal -= turnIncrement * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(new Vector3(dir.normalized.x, (transform.position.y - stopTurnY) / (stopTurnY - startTurnY) * turnConst, dir.normalized.z));
                print("Turn value: " + turnVal);

                GetComponent<Rigidbody>().useGravity = true;
                //rb.constraints = RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                rb.velocity = transform.forward * sealSpeed;
                transform.rotation = Quaternion.LookRotation(new Vector3(dir.normalized.x, turnConst - (transform.position.y - startTurnY) / (stopTurnY - startTurnY) * turnConst, dir.normalized.z));
            }

            //when it's submerged, destroy it.
            if (transform.position.y < -1.5f)
            {
                Destroy(gameObject);
            }

            /*
            if (transform.position.y < startTurnY)
            {

                transform.rotation = Quaternion.LookRotation(new Vector3(dir.x,dir.y+ turnConst, dir.z));
            }
            else
            {
                
            }*/

        }

        /*
        if(state == STATE.LANDFALL)
        {
            //seal is going on land after spawning in water. 
            rb.velocity = new Vector3(0, sealSpeed, 0) + transform.forward * sealSpeed;
            if(transform.position.y >= 1f) {
                state = STATE.ON_LAND;
                rb.constraints = RigidbodyConstraints.FreezePositionY | rb.constraints;
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
        */
    }

    private bool IsOnIce()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, maxIceDistance);

        foreach (Collider nearbyCollider in nearbyColliders)
        {
            if (nearbyCollider.CompareTag("Ice"))
            {
                beenOnLand = true;
                return true;
            }
        }
        return false;
    }
}
