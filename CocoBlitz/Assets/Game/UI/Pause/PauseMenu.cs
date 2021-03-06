﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameAudioManager gameAudioManager;

    public Text resumeStats_txt;


    public GameObject cardContainer;
    public GameObject inGameUiContainer;
    public GameObject pauseUiContainer;
    public GameObject scoreUiContainer;
    private List<Statistics> statsList;


    public BananaRewardWindowManager bananaRewardWindowManager;
    public GameObject statsContainer;
    public GameObject mainStatsContainer;
    public Text averageTimeElapsedText;
    public Text averageTimeElapsedForCorrectOnesText;
    public Text averageTimeElapsedForCorrectOnesWithCorrectlyColoredText;
    public Text averageTimeElapsedForCorrectOnesWithIncorrectlyColoredText;
    public Text averageTimeElapsedForIncorrectOnesText;
    public Text averageTimeElapsedForIncorrectOnesWithCorrectlyColoredText;
    public Text averageTimeElapsedForIncorrectOnesWithIncorrectlyColoredText;
    public Text totalText;
    public Text totalCorrectText;
    public Text totalCorrectWithCorrectlyColoredText;
    public Text totalCorrectWithIncorrectlyColoredText;
    public Text totalIncorrectText;
    public Text totalIncorrectWithCorrectlyColoredText;
    public Text totalIncorrectWithIncorrectlyColoredText;
    public Text totalMissedText;
    public Text totalMissedWithCorrectlyColoredText;
    public Text totalMissedWithIncorrectlyColoredText;

    public GameObject statsPointContainer;
    public Text timeElapsedText;
    public Text correctEntityText;
    public Text guessedEntityText;
    public Text messageText;

    public GameObject singlePlayerGroup;
    public GameObject twoPlayersGroup;

    public Toggle player1StatsToggle;
    public GameObject playerStatsToggleContainer;

    private int statsIndex;
    private Card card;
    void Start()
    {

        singlePlayerGroup.SetActive(GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Single);
        twoPlayersGroup.SetActive(GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two);
        statsIndex = 0;
        Pause(false,false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Pause(true);
        }
    }

    public void GameOver(List<Statistics> statsList)
    {
        resumeStats_txt.text = "Stats";
        this.statsList = statsList;
        ShowPauseContainer(true);
        gameAudioManager.FadeOutBackgroundMusic();
    }

    public void Pause(bool pause)
    {
        Pause(pause, true);
    }

    public void Pause(bool pause, bool playSfx)
    {
        if (playSfx)
            gameAudioManager.PlayMainButtonClick();
        if (statsList != null)
        {
            StringBuilder builder = new StringBuilder();
            statsList.ForEach(stats =>
                {
                    string statsForPrint = stats.GetStatsForPrint();
                    builder.Append(statsForPrint);
                    Debug.Log(statsForPrint);
                }
            );
            ShowStatsContainer(true);

        }
        else
        {

            resumeStats_txt.text = "Resume";
            Time.timeScale = pause ? 0f : 1f;
            ShowPauseContainer(pause);

            if (pause)
            {
                gameAudioManager.FadeOutBackgroundMusic();
            }
            else
            {
                gameAudioManager.FadeInBackgroundMusic();
            }
        }
    }

    //Value doesn't matter
    public void PlayerStatsToggleHandler(bool value)
    {
        StepStats(0);
    }

    private void ShowStatsContainer(bool show)
    {

        statsContainer.SetActive(show);
        inGameUiContainer.SetActive(false);
        pauseUiContainer.SetActive(!show);
        scoreUiContainer.SetActive(!show);
        cardContainer.SetActive(show);


        if (card != null) { 
            DestroyImmediate(card.gameObject);
            card = null;
        }

        playerStatsToggleContainer.SetActive(GameSettingsUtil.GetGameType() != GameSettingsUtil.GameTypeEnum.Single);
        

        Statistics playerStats = GetPlayerStats();

        if (show)
        {
            if (statsIndex == 0)
            {
                mainStatsContainer.SetActive(true);
                statsPointContainer.SetActive(false);
                averageTimeElapsedText.text = playerStats.Total == 0 ? "-" : playerStats.AverageTimeElapsed.ToString("0.00") + " s";
                averageTimeElapsedForCorrectOnesText.text = playerStats.TotalCorrect == 0 ? "-" : playerStats.AverageTimeElapsedForCorrectOnes.ToString("0.00") + " s";
                averageTimeElapsedForCorrectOnesWithCorrectlyColoredText.text = playerStats.TotalCorrectWithCorrectlyColored == 0 ? "-" : playerStats.AverageTimeElapsedForCorrectOnesWithCorrectlyColored.ToString("0.00") + " s";
                averageTimeElapsedForCorrectOnesWithIncorrectlyColoredText.text = playerStats.TotalCorrectWithIncorrectlyColored == 0 ? "-" : playerStats.AverageTimeElapsedForCorrectOnesWithIncorrectlyColored.ToString("0.00") + " s";
                averageTimeElapsedForIncorrectOnesText.text = playerStats.TotalIncorrect==0 ? "-" : playerStats.AverageTimeElapsedForIncorrectOnes.ToString("0.00") + " s";
                averageTimeElapsedForIncorrectOnesWithCorrectlyColoredText.text = playerStats.TotalIncorrectWithCorrectlyColored==0 ? "-" : playerStats.AverageTimeElapsedForIncorrectOnesWithCorrectlyColored.ToString("0.00") + " s";
                averageTimeElapsedForIncorrectOnesWithIncorrectlyColoredText.text = playerStats.TotalIncorrectWithIncorrectlyColored == 0 ? "-" : playerStats.AverageTimeElapsedForIncorrectOnesWithIncorrectlyColored.ToString("0.00") + " s";
                totalText.text = playerStats.Total.ToString();
                totalCorrectText.text = playerStats.TotalCorrect.ToString();
                totalCorrectWithCorrectlyColoredText.text = playerStats.TotalCorrectWithCorrectlyColored.ToString();
                totalCorrectWithIncorrectlyColoredText.text = playerStats.TotalCorrectWithIncorrectlyColored.ToString();
                totalIncorrectText.text = playerStats.TotalIncorrect.ToString();
                totalIncorrectWithCorrectlyColoredText.text = playerStats.TotalIncorrectWithCorrectlyColored.ToString();
                totalIncorrectWithIncorrectlyColoredText.text = playerStats.TotalIncorrectWithIncorrectlyColored.ToString();

                //If just one player, dont display misses
                bool onePlayer = statsList.Count <= 1;
                totalMissedText.text = onePlayer ? "-" : playerStats.TotalMissed.ToString();
                totalMissedWithCorrectlyColoredText.text = onePlayer ? "-" : playerStats.TotalMissedWithCorrectlyColored.ToString();
                totalMissedWithIncorrectlyColoredText.text = onePlayer ? "-" : playerStats.TotalMissedWithIncorrectlyColored.ToString();
            }
            else
            {
                mainStatsContainer.SetActive(false);
                statsPointContainer.SetActive(true);
                
                StatisticsPoint statsPoint = playerStats.StatisticsList[statsIndex - 1];
                card = Instantiate(statsPoint.Card, cardContainer.transform);
                card.gameObject.SetActive(true);

                Array.ForEach(card.cardEntities, cardEntity =>
                {

                    CardUtil.ColorEnum color = statsPoint.EntityToColor[cardEntity.entity];
                    cardEntity.image.color = CardUtil.ColorColorMap[color];
                });
                

                timeElapsedText.text = statsPoint.Missed ? "-" : statsPoint.TimeElapsed.ToString("0.00") + " s";
                correctEntityText.text = statsPoint.CorrectEntity.ToString();
                guessedEntityText.text = statsPoint.Missed ? "Missed": statsPoint.GuessedEntity.ToString();
                messageText.text = "Card " + statsIndex;

            }
        }
    }

    private Statistics GetPlayerStats()
    {
        bool forPlayer2 = !player1StatsToggle.isOn;
        //Assume first element is player1 and second element is player 2 IF game type is for 2 players.
        return GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two && forPlayer2 ? statsList[1] : statsList[0];
    }

    private void ShowPauseContainer(bool pause)
    {        
        inGameUiContainer.SetActive(!pause);
        pauseUiContainer.SetActive(pause);
        scoreUiContainer.SetActive(true);
        statsContainer.SetActive(false);
        cardContainer.SetActive(!pause);
    }

    public void Restart()
    {
        gameAudioManager.PlayMainButtonClick();
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        gameAudioManager.PlayMainButtonClick();
        GameUtil.fromGame = true;
        SceneManager.LoadScene("Load Screen");
    }

    public void BackFromStats()
    {
        gameAudioManager.PlayMainButtonClick();
        ShowStatsContainer(false);
        ShowPauseContainer(true);
    }

    public void StepStats(int step)
    {
        if (statsList != null) {
            gameAudioManager.PlayCardFlip();

            Statistics playerStats = GetPlayerStats();

            if (step >= 0)
            {
                if (playerStats.StatisticsList.Count >= statsIndex + step)
                {
                    statsIndex += step;
                    ShowStatsContainer(true);
                }
                else
                {
                    EndStats(true);
                }
            }
            else
            {

                if (0 <= statsIndex + step)
                {
                    statsIndex += step;
                    ShowStatsContainer(true);
                }
                else
                {
                    EndStats(false);
                }
            }
       }
    }

    //last or first
    public void EndStats(bool last)
    {
        if (statsList != null)
        {
            gameAudioManager.PlayCardFlip();
            Statistics playerStats = GetPlayerStats();
            int calculatedStatsIndex = last ? playerStats.StatisticsList.Count : 0;

            //If there is a change in the statsIndex
            if (calculatedStatsIndex - statsIndex != 0)
            {
                statsIndex = calculatedStatsIndex;
                ShowStatsContainer(true);
            }
        }
    }

}
