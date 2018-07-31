using System.Collections;
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
    public GameObject Player1Seals;
    public GameObject Player2Seals;

    public GameDataScript gameDataScript; //keeps track of points and passes it on to the end screen
    
    //update respawn time for a player
    public void RespawnTime(int player, float time)
    {
        respawnTimes[player-1] = time;
        Draw();
        if (respawnTimes[0] > 0)
        {

            P1Text.text = "You respawn in: " + Mathf.CeilToInt(respawnTimes[0]);
        }
        if (respawnTimes[1] > 0)
        {

            P2Text.text = "You respawn in: " + Mathf.CeilToInt(respawnTimes[1]);
        }
    }

    public void Update()
    {
        //draw game time
        int totalTime = Mathf.CeilToInt((gameLength + startTime) - Time.time);
        int minutes = (int)Mathf.Floor(totalTime / 60f);
        int seconds = totalTime % 60;

        if (Input.GetAxis("Cancel")>0) {
            //escape the map
            SceneManager.LoadScene("StartScene");
        }

        if(totalTime < 0)
        {
            //Overtime
            gameTimer.text = "OVERTIME";
        }
        else{
            //normal time
            gameTimer.text = Add0s(minutes) + ":" + Add0s(seconds);
        }

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
        if (totalTime <= 0 && points[0] != points[1])
        {
            //SceneManager.LoadScene("EndScene");
            if (points[0] > points[1])
            {
                SceneManager.LoadScene("EndScene Lilla"); //p1 er lilla

            }
            else
            {
                SceneManager.LoadScene("EndScene Grøn"); //p2 er groen
            }
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
        SealPoints(playerNumber);
    }
    

    private void Draw()
    {
        P1Text.text = "";
        P2Text.text = "";
    }


    private void SealPoints(int PlayerNumber)
    {
        PointSealScript[] pointSeals;
        if (PlayerNumber == 1)
        {
            pointSeals = Player1Seals.GetComponentsInChildren<PointSealScript>();

            foreach(PointSealScript p in pointSeals)
            {
                if (p.SealNumber == points[PlayerNumber - 1])
                {
                    p.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
                }
            }
        }

        if (PlayerNumber == 2)
        {
            pointSeals = Player2Seals.GetComponentsInChildren<PointSealScript>();

            foreach (PointSealScript p in pointSeals)
            {
                if (p.SealNumber == points[PlayerNumber - 1])
                {
                    p.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
                }
            }
        }
    }
}
