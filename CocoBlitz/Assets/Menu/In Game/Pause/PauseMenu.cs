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
    private List<Statistics> statsList;

    void Start()
    {
        inGameUIGameObjects = GameObject.FindGameObjectsWithTag("In Game UI");
        pauseUIGameObjects = GameObject.FindGameObjectsWithTag("Pause UI");
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

            EmailService.SendEmail(builder.ToString());
        }
        else
        {

            resumeStats_txt.text = "Resume";
            Time.timeScale = pause ? 0f : 1f;
            ShowProperGameObjects(pause);
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
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main");
    }

}
