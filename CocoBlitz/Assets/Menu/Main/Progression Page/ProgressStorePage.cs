using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStorePage : MonoBehaviour, Page
{
    public Text instructions;
    public Text bananaCount;
    public ProgressContent progressContent;
    public StoreContent storeContent;
    public GameObject titleArea;
    // Use this for initialization
    void Start()
    {
        UpdateBananaCount();
    }

    public void UpdateBananaCount()
    {

        bananaCount.text = "x" + GameProgressionUtil.GetBananas().ToString();
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
        storeContent.gameObject.SetActive(progressionPageEnum == ProgressionPage.ProgressionPageEnum.CocoStore );
    }
}
