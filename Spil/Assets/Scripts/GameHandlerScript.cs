using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandlerScript : MonoBehaviour {

    public int numberOfPlayers = 2;

    public int[] points;
    public Text pointsText;

    public void Start()
    {
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

        string s = "";
        for(int i =0; i < points.Length; i++)
        {
            s += "Player " + (i + 1) + ": " + points[i] + " ";
        }

        pointsText.text = s;
    }

}
