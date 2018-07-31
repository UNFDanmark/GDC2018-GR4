using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour {

    public enum STATE
    {
        ALIVE,
        SHRINKING,
        TILTING,
        SINKING,
        DEAD
    }

    public GameObject snapSound;

    public STATE state = STATE.ALIVE;

    public float shrinkingTime = 5f,
        tiltingTime = 0f,
        sinkingTime = 5f;

    public float shrinkFactor = 0.95f,
        tiltFactor = 0f,
        sinkFactor = 0.99f;

    public GameObject[] killBoxes;

    private float deltaTime = 0; //time since sinking began

    private Vector3 localScale;
    private Vector3 dir;

    private void SetState(STATE state)
    {
        this.state = state;
        print("new state: " + state);
        deltaTime = 0;
        if(state==STATE.SHRINKING) snapSound.GetComponent<AudioSource>().Play();
        if (state == STATE.SINKING)
        {
            //spawn killboxes
            foreach (GameObject box in killBoxes)
            {
                box.transform.position = new Vector3(box.transform.position.x, 0f, box.transform.position.z);
            }
        }
    }

    public void StartSinking()
    {
        SetState(STATE.SHRINKING);
        localScale = gameObject.transform.localScale;
        dir = transform.forward;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.P))
        {
            StartSinking();
        }

        deltaTime += Time.deltaTime;
        if (state == STATE.SHRINKING)
        {

            float shrink = (shrinkFactor - 1) / shrinkingTime * deltaTime + 1;
            gameObject.transform.localScale = new Vector3(localScale.x * shrink, localScale.y*shrink, localScale.z);

            if(deltaTime > shrinkingTime)
            {
                SetState(STATE.SINKING);
                localScale = gameObject.transform.localScale;

            }
        }else if(state == STATE.SINKING)
        {

            float sink = (sinkFactor - 1) / sinkingTime * deltaTime + 1;
            gameObject.transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z*sink);
            if(deltaTime > sinkingTime)
            {
                SetState(STATE.DEAD);
            }

        }
    }
}
