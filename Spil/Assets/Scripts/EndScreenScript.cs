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
        
        if (points[0] == points[1])
        {
            //both players had equal points when time ran out
            //TODO make sudden death mode?

            winMessage = "DRAW";
        }else if (points[0] > points[1])
        {
            //player 1 had the most points
            winMessage = "Player 1 won!";
        }
        else
        {
            //player 2 had the most points
            winMessage = "Player 2 won!";
        }
        winMessage += "\nPlayer 1: " + points[0] + " Player 2: " + points[1];
        message.text = winMessage;

        //add listener for buttons
        restart.onClick.AddListener(OnClickRestart);
    }

    public void OnClickRestart()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameData"));
        SceneManager.LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
