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
        //add win message
        int[] points = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>().points;

        string winMessage = "";
        print(points[0] + " " + points[1]);
        if (points[0] == points[1])
        {
            //both players had equal points when time ran out. this does not happen.
            
            winMessage += "";
        }else if (points[0] > points[1])
        {
            //player 1 (purple) had the most points
            winMessage = "green: " + points[0] + "\nPurple: " + points[1];
        }
        else
        {
            //player 2 (green) had the most points
            winMessage = "Purple: " + points[1] + "\nGreen: " + points[0];
        }
        message.text = winMessage;

        //add listener for buttons
        restart.onClick.AddListener(OnClickRestart);
    }

    public void OnClickRestart()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameData"));
        SceneManager.LoadScene("MainScene efter is");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
