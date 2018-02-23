using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStorePage : MonoBehaviour, Page
{
    public Text instructions;
    public ProgressContent progressContent;
    public GameObject titleArea;
    // Use this for initialization
    void Start()
    {

    }



    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
        titleArea.SetActive(!activate);
    }

    public void SetPageType(ProgressionPage.ProgressionPageEnum progressionPageEnum)
    {
        if (progressionPageEnum== ProgressionPage.ProgressionPageEnum.BananaProgress)
        {
            instructions.text = "Earn bananas while playing Coco Go!";
        } else
        {

            instructions.text = "Unlock cards & characters with your hard earned bananas.";
        }

        progressContent.gameObject.SetActive(progressionPageEnum == ProgressionPage.ProgressionPageEnum.BananaProgress);
    }
}
