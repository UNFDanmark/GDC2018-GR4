using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour {

    public int[] points;
    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        points = new int[] { 0, 0 };
        DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
