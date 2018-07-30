﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandlerScript : MonoBehaviour {

    public int numberOfPlayers = 2;

    public int[] points;
    public Text P1Text, P2Text, gameTimer;

    private float startTime;
    public float gameLength;
    float breakIce1 = 80,
        breakIce2 = 40;
    private bool iceBroken1, iceBroken2;


    public GameObject[] iceHoles;

    float[] respawnTimes = new float[2];


    public GameDataScript gameDataScript; //keeps track of points and passes it on to the end screen
    
    //update respawn time for a player
    public void RespawnTime(int player, float time)
    {
        respawnTimes[player-1] = time;
        Draw();
        if (respawnTimes[0] > 0)
        {

            P1Text.text += "\nYou respawn in: " + Mathf.CeilToInt(respawnTimes[0]);
        }
        if (respawnTimes[1] > 0)
        {

            P2Text.text += "\nYou respawn in: " + Mathf.CeilToInt(respawnTimes[1]);
        }
    }

    public void Update()
    {
        //draw game time
        int totalTime = Mathf.CeilToInt((gameLength + startTime) - Time.time);
        int minutes = (int)Mathf.Floor(totalTime / 60f);
        int seconds = totalTime % 60;

        gameTimer.text = Add0s(minutes) + ":" + Add0s(seconds);
        if (totalTime < breakIce1 && !iceBroken1)
        {
            iceBroken1 = true;
            //break ice
            iceHoles[0].GetComponent<IceScript>().StartSinking();
        }
        if (totalTime < breakIce2 && !iceBroken2)
        {
            iceBroken2 = true;
            //break ice
            iceHoles[1].GetComponent<IceScript>().StartSinking();
        }
        if (totalTime <= 0)
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    private string Add0s(int num)
    {
        string text = "" + num;
        while(text.Length < 2)
        {
            text = "0" + text;
        }
        return text;
    }

    public void Start()
    {
        startTime = Time.time;
        points = new int[numberOfPlayers];
        Draw();

        //shuffle the pieces in the list
        iceHoles = Reshuffle(iceHoles);
    }

    GameObject[] Reshuffle(GameObject[] list)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < list.Length; t++)
        {
            GameObject tmp = list[t];
            int r = Random.Range(t, list.Length);
            list[t] = list[r];
            list[r] = tmp;
        }
        return list;
    }

    public void AddPoint(int playerNumber)
    {
        points[playerNumber-1]++;
        Draw();
        gameDataScript.points = points;
    }
    

    private void Draw()
    {
        P1Text.text = "Player 1: " + points[0] + " ";
        P2Text.text = "Player 2: " + points[1] + " ";
    }

}
