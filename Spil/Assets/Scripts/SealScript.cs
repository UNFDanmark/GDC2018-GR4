using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealScript : MonoBehaviour {
    public Rigidbody rb;

    public float sealSpeed;

    public float maxIceDistance = 10;

    public Vector3 target;
    private Vector3 dir;
    private Vector3 dirNorm;
    
    public float despawnDepth = -2f;
    private float startTurnY = 0.3f; //height for beginning of rotation 
    private float stopTurnY = 0.5f; //height for end of rotation (surface level)

    private float turnConst = 1.7f; //convenient constant for the rotation when entering and leaving the ice
    
    public void SetTarget(Vector3 target)
    {
        this.target = target;
        dir = (target - transform.position);

        MoveTowardsIce();
    }

    private enum STATE
    {
        NORMAL,
        DIVING
    }
    STATE state = STATE.NORMAL;

    public void MoveTowardsIce()
    {
        int max = 50;
        transform.rotation = Quaternion.LookRotation(dir);
        while (!TouchesIce())
        {
            Vector3 newPos = transform.position + dir * 0.01f;
            rb.position = (newPos);
            print("Moved seal " + max);
            max--;
            if (max < 1) return;
        }

        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("KillBox") && beenOnLand)
        {
            state = STATE.DIVING;
            //dive
        }
    }

    private bool TouchesIce()
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

    private bool beenOnLand = false;

    // Update is called once per frame
    void Update()
    {
        //if it's close to the edge of the screen, turn on gravity so it can fall.
        if ((!IsOnIce() && beenOnLand) || state == STATE.DIVING)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(dir.normalized.x, (transform.position.y - stopTurnY) / (stopTurnY - startTurnY) * turnConst, dir.normalized.z));

            GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            //move normally
            rb.velocity = transform.forward * sealSpeed;
            transform.rotation = Quaternion.LookRotation(new Vector3(dir.normalized.x, turnConst - (transform.position.y - startTurnY) / (stopTurnY - startTurnY) * turnConst, dir.normalized.z));
        }

        //when it's submerged, destroy it.
        if (transform.position.y < despawnDepth)
        {
            Destroy(gameObject);
        }
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
