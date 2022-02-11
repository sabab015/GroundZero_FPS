﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firsLevel;

    public GameObject continueButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentKey"))
        {
            if (PlayerPrefs.GetString("CurrentLevel") == "")
            {
                continueButton.SetActive(false);
            }
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentLevel"));
    }

    public void PlayGame()
    {

        SceneManager.LoadScene(firsLevel);
        PlayerPrefs.SetString("CurrentLevel", "");

        PlayerPrefs.SetString(firsLevel + "_cp", "");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game");
    }
}
