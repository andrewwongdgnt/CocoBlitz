using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject mainPage;
    public GameObject gamePage;

    public Text instructions_txt;
    public Dropdown gameOptions_dropDown;

    private GameManager.GameModeEnum currentGameMode;
    private int pointsToReach;
    private float timer;

    void Start()
    {
        mainPage.SetActive(true);
        gamePage.SetActive(false);
    }
    public void SelectGameMode(GameManager.GameModeEnum gameMode)
    {
        mainPage.SetActive(false);
        gamePage.SetActive(true);
        currentGameMode = gameMode;
        UpdateGameParams(currentGameMode, gameOptions_dropDown.options[gameOptions_dropDown.value].text);

    }

    public void DropDownChanged(int i)
    {
        string value = gameOptions_dropDown.options[i].text;
        UpdateGameParams(currentGameMode, value);
    }

    public void Back()
    {
        mainPage.SetActive(true);
        gamePage.SetActive(false);
    }

    public void PlayGame()
    {
        GameManager.currentGameMode = currentGameMode;
        GameManager.pointsToReach = pointsToReach;
        GameManager.timer = timer;
        SceneManager.LoadScene("Game");
    }

    void UpdateGameParams(GameManager.GameModeEnum gameMode, string value)
    {
        if (gameMode == GameManager.GameModeEnum.FastestTime)
        {
            instructions_txt.text = "Get " + value + " correct as fast as you can!";
            instructions_txt.fontSize = 100;
            pointsToReach = int.Parse(value);
        }
        else
        {
            instructions_txt.text = "Get as many correct as possible within " + value + " seconds!";
            instructions_txt.fontSize = 85;
            timer = float.Parse(value);
        }
    }

}
