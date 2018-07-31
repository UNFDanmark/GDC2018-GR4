using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour {


    public Text message;
    public Button restart;

	// Use this for initialization
    public void Start()
    {

        //add listener for buttons
        restart.onClick.AddListener(OnClickRestart);
        if (GameObject.FindGameObjectWithTag("GameData") == null) return;
        //add win message

        int[] points = null;

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("GameData"))
        {
            int[] v = g.GetComponent<GameDataScript>().points;
            if (v[0] != v[1]) points = v;
        }
        
        string winMessage = "";
        print(points[0] + " " + points[1]);
        if (points[0] == points[1])
        {
            //both players had equal points when time ran out. this does not happen.
            
            winMessage += "";
        }else if (points[0] > points[1])
        {
            //player 1 (purple) had the most points
            winMessage = "Purple: " + points[0] + "\nGreen: " + points[1];
        }
        else
        {
            //player 2 (green) had the most points
            winMessage = "Green: " + points[1] + "\nPurple: " + points[0];
        }
        message.text = winMessage;

    }

    public void OnClickRestart()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("GameData"))
        {
            Destroy(g);
        }   
        SceneManager.LoadScene("MainScene efter is");
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetAxis("Cancel") > 0)
        {
            //escape the map
            Application.Quit();
        }
    }
}
