using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public Text resumeStats_txt;

    private GameObject[] cardGameObjects;
    private GameObject[] inGameUIGameObjects;
    private GameObject[] pauseUIGameObjects;
    private GameObject[] scoreUIGameObjects;
    private List<Statistics> statsList;

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

    private int statsIndex;
    private Card card;
    void Start()
    {

        singlePlayerGroup.SetActive(GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_SINGLE_PLAYER);
        twoPlayersGroup.SetActive(GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS);

        inGameUIGameObjects = GameObject.FindGameObjectsWithTag("In Game UI");
        pauseUIGameObjects = GameObject.FindGameObjectsWithTag("Pause UI");
        scoreUIGameObjects = GameObject.FindGameObjectsWithTag("Score UI");
        statsIndex = 0;
        Pause(false);
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
        ShowProperGameObjects(true);
    }



    public void Pause(bool pause)
    {
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
            //EmailService.SendEmail(builder.ToString());
           // statsIndex = 0;
            ShowStatsContainer(true);
            
        }
        else
        {

            resumeStats_txt.text = "Resume";
            Time.timeScale = pause ? 0f : 1f;
            ShowProperGameObjects(pause);
        }
    }

    private void ShowStatsContainer(bool show)
    {

        statsContainer.SetActive(show);
        Array.ForEach(inGameUIGameObjects, ent => ent.SetActive(false));
        Array.ForEach(pauseUIGameObjects, ent => ent.SetActive(!show));

        if (cardGameObjects != null)
            Array.ForEach(cardGameObjects, ent => ent.SetActive(false));

        Array.ForEach(scoreUIGameObjects, ent => ent.SetActive(!show));


        if (card != null) { 
            DestroyImmediate(card.gameObject);
            card = null;
        }

        //Assume first element is player
        Statistics playerStats = statsList[0];

        if (show)
        {
            if (statsIndex == 0)
            {
                mainStatsContainer.SetActive(true);
                statsPointContainer.SetActive(false);
                averageTimeElapsedText.text = playerStats.AverageTimeElapsed.ToString("0.00") + " s";
                averageTimeElapsedForCorrectOnesText.text = playerStats.AverageTimeElapsedForCorrectOnes.ToString("0.00") + " s";
                averageTimeElapsedForCorrectOnesWithCorrectlyColoredText.text = playerStats.AverageTimeElapsedForCorrectOnesWithCorrectlyColored.ToString("0.00") + " s";
                averageTimeElapsedForCorrectOnesWithIncorrectlyColoredText.text = playerStats.AverageTimeElapsedForCorrectOnesWithIncorrectlyColored.ToString("0.00") + " s";
                averageTimeElapsedForIncorrectOnesText.text = playerStats.AverageTimeElapsedForIncorrectOnes.ToString("0.00") + " s";
                averageTimeElapsedForIncorrectOnesWithCorrectlyColoredText.text = playerStats.AverageTimeElapsedForIncorrectOnesWithCorrectlyColored.ToString("0.00") + " s";
                averageTimeElapsedForIncorrectOnesWithIncorrectlyColoredText.text = playerStats.AverageTimeElapsedForIncorrectOnesWithIncorrectlyColored.ToString("0.00") + " s";
                totalText.text = playerStats.Total.ToString();
                totalCorrectText.text = playerStats.TotalCorrect.ToString();
                totalCorrectWithCorrectlyColoredText.text = playerStats.TotalCorrectWithCorrectlyColored.ToString();
                totalCorrectWithIncorrectlyColoredText.text = playerStats.TotalCorrectWithIncorrectlyColored.ToString();
                totalIncorrectText.text = playerStats.TotalIncorrect.ToString();
                totalIncorrectWithCorrectlyColoredText.text = playerStats.TotalIncorrectWithCorrectlyColored.ToString();
                totalIncorrectWithIncorrectlyColoredText.text = playerStats.TotalIncorrectWithIncorrectlyColored.ToString();

                //If no cpus, dont display misses
                bool noCpus = statsList.Count <= 1;
                totalMissedText.text = noCpus ? "-" : playerStats.TotalMissed.ToString();
                totalMissedWithCorrectlyColoredText.text = noCpus ? "-" : playerStats.TotalMissedWithCorrectlyColored.ToString();
                totalMissedWithIncorrectlyColoredText.text = noCpus ? "-" : playerStats.TotalMissedWithIncorrectlyColored.ToString();
            }
            else
            {
                mainStatsContainer.SetActive(false);
                statsPointContainer.SetActive(true);
                StatisticsPoint statsPoint = playerStats.StatisticsList[statsIndex - 1];
                card = Instantiate(statsPoint.Card);
                card.gameObject.SetActive(true);

                Array.ForEach(card.cardEntities, cardEntity =>
                {

                    CardUtil.ColorEnum color = statsPoint.EntityToColor[cardEntity.entity];
                    cardEntity.spriteRenderer.color = CardUtil.ColorColorMap[color];
                });
                

                timeElapsedText.text = statsPoint.Missed ? "-" : statsPoint.TimeElapsed.ToString("0.00") + " s";
                correctEntityText.text = statsPoint.CorrectEntity.ToString();
                guessedEntityText.text = statsPoint.Missed ? "Missed": statsPoint.GuessedEntity.ToString();
                messageText.text = "Card " + statsIndex;

            }
        }
    }

    private void ShowProperGameObjects(bool pause)
    {
        if (pause)
            cardGameObjects = GameObject.FindGameObjectsWithTag("Card");
        if (cardGameObjects != null)
            Array.ForEach(cardGameObjects, ent => ent.SetActive(!pause));
        Array.ForEach(inGameUIGameObjects, ent => ent.SetActive(!pause));
        Array.ForEach(pauseUIGameObjects, ent => ent.SetActive(pause));
        statsContainer.SetActive(false);
        Array.ForEach(scoreUIGameObjects, ent => ent.SetActive(true));
        

    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        GameUtil.fromGame = true;
        SceneManager.LoadScene("Main");
    }

    public void BackFromStats()
    {
        ShowStatsContainer(false);
        ShowProperGameObjects(true);
    }

    public void NextStats()
    {
        if (statsList != null) {

            Statistics playerStats = statsList[0];

            if (playerStats.StatisticsList.Count > statsIndex)
            {
                statsIndex++;
                ShowStatsContainer(true);
            }
       }
    }



    public void PrevStats()
    {
        if (statsList != null)
        {

            Statistics playerStats = statsList[0];

            if (statsIndex > 0)
            {
                statsIndex--;
                ShowStatsContainer(true);
            }
        }
    }

}
