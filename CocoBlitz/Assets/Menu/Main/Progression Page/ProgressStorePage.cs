using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStorePage : MonoBehaviour, Page
{
    public MenuAudioManager menuAudioManager;
    public Text instructions;
    public Text bananaCount;
    public ProgressContent progressContent;
    public StoreContent storeContent;
    public GameObject titleArea;

    public Animator bananaTextAnim;

    public IAPWindowDisplay iapWindowDisplay;
    // Use this for initialization
    void Start()
    {
        UpdateBananaCount();
    }

    public void UpdateBananaCount()
    {

        bananaCount.text = "x" + GameProgressionUtil.GetBananas().ToString();
    }

    public void DisplayNotEnough()
    {
        bananaTextAnim.SetTrigger("NotEnough");


    }



    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
        titleArea.SetActive(!activate);
        iapWindowDisplay.SetActive(false);
    }

    public void SetPageType(ProgressionPage.ProgressionPageEnum progressionPageEnum)
    {
        if (progressionPageEnum== ProgressionPage.ProgressionPageEnum.BananaProgress)
        {
            instructions.text = "Earn bananas while playing Coco Go!";
        } else
        {

            instructions.text = "Unlock cards & friends with your hard earned bananas.";
        }

        progressContent.SetActive(progressionPageEnum == ProgressionPage.ProgressionPageEnum.BananaProgress);
        storeContent.SetActive(progressionPageEnum == ProgressionPage.ProgressionPageEnum.CocoStore );
    }



    public void ShowIapWindow()
    {
        menuAudioManager.PlayMainButtonClick();
        iapWindowDisplay.SetActive(true);
    }

    public void HideIapWindow()
    {
        menuAudioManager.PlayMainButtonClick();
        iapWindowDisplay.SetActive(false);
    }
}
