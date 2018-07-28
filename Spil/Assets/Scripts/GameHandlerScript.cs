using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandlerScript : MonoBehaviour {

    public int numberOfPlayers = 2;

    public int[] points;
    public Text P1Text, P2Text, gameTimer;

    private float startTime;
    public float gameLength;

    public void RespawnTime(int player, float time)
    {
        Draw();
        if (time <= 0) return;
        if(player == 1)
        {
            P1Text.text += "\nYou respawn in: " + Mathf.CeilToInt(time);
        }
        else
        {
            P2Text.text += "\nYou respawn in: " + Mathf.CeilToInt(time);
        }
    }

    public void Update()
    {
        //draw game time
        int totalTime = Mathf.CeilToInt((gameLength + startTime) - Time.time);
        int minutes = (int)Mathf.Floor(totalTime / 60f);
        int seconds = totalTime % 60;

        gameTimer.text = Add0s(minutes) + ":" + Add0s(seconds);

        if(totalTime <= 0)
        {
            //Game over!!!
            //TODO if players have same number of points, say something else
            gameTimer.text = "GAME OVER!\n" + (points[0] > points[1] ? "PLAYER 1 WON" : "PLAYER 2 WON");
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
    }

    public void AddPoint(int playerNumber)
    {
        points[playerNumber-1]++;
        Draw();
    }
    
    //this is seal

    private void Draw()
    {
        P1Text.text = "Player 1: " + points[0] + " ";
        P2Text.text = "Player 2: " + points[1] + " ";
    }

}
