using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public Text resumeStats_txt;

    private GameObject[] cardGameObjects;
    private GameObject[] inGameUIGameObjects;
    private GameObject[] pauseUIGameObjects;
    private Statistics stats;

    void Start()
    {
        inGameUIGameObjects = GameObject.FindGameObjectsWithTag("In Game UI");
        pauseUIGameObjects = GameObject.FindGameObjectsWithTag("Pause UI");
        Pause(false);
    }

    public void GameOver(Statistics stats)
    {
        resumeStats_txt.text = "Stats";
        this.stats = stats;
        ShowProperGameObjects(true);
    }

    public void Stats()
    {
        if (stats != null)
        {
            Debug.Log(stats.GetStatsForPrint());
        }
    }

    public void Pause(bool pause)
    {
        if (stats != null)
        {
            Debug.Log(stats.GetStatsForPrint());
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
