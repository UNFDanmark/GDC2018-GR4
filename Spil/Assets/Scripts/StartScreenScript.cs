﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenScript : MonoBehaviour {

    public Button restart;

    // Use this for initialization
    public void Start()
    {
        print("sup");

        //add listener for buttons
        restart.onClick.AddListener(OnClickRestart);
    }

    public void Update()
    {
        if (Input.GetAxis("Cancel") > 0)
        {
            //escape the map
            Application.Quit();
        }
    }

    public void OnClickRestart()
    {
        print("Hey");
        SceneManager.LoadScene("MainScene efter is");


    }

}
