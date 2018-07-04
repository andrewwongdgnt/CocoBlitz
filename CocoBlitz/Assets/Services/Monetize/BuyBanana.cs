using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;

using UnityEngine;
using System;
using UnityEngine.UI;

public class BuyBanana : MonoBehaviour {


    public enum ProductIdEnum { Banana1, Banana2, Banana3, Banana4, Banana5, Banana6};

    public ProductIdEnum productIdEnum;
    public int costInCents;
    public Text costInCentsTxt;
    public int bananaCount;
    public Text bananaCountTxt;
    public ProgressStorePage progressStorePage;
    public IAPWindowDisplay iapWindowDisplay;
    public MenuAudioManager menuAudioManager;
    public IAPManager iapManager;

    void Start()
    {
        if  (productIdEnum!= ProductIdEnum.Banana1) {

            float costInDollar = costInCents / 100f;
            costInCentsTxt.text = "$"+costInDollar.ToString();
        }
        
        bananaCountTxt.text = bananaCount.ToString();
    }

    void Update()
    {
        if (productIdEnum == ProductIdEnum.Banana1)
        {
            int nextTimeToWatch = AdUtil.GetNextTimeToWatch();
            if (nextTimeToWatch == 0)
            {
                costInCentsTxt.text = "Watch Ad";
            }
            else
            {
                string nextTimeToWatchFormatted = TransformTime(nextTimeToWatch);
                costInCentsTxt.text = "Next Ad in " + nextTimeToWatchFormatted;
            }
        }
    }

    private string TransformTime(int timeInSeconds)
    {

        TimeSpan t = TimeSpan.FromSeconds(timeInSeconds);

        return string.Format("{0:D1}:{1:D2}:{2:D2}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds);
    }

    public void Buy()
    {
        if (productIdEnum == ProductIdEnum.Banana1 && AdUtil.GetNextTimeToWatch() == 0)
        {
            bool successfulAdShown = false;
            menuAudioManager.PlayMainButtonClick();
            successfulAdShown = ShowAd();
            if (successfulAdShown)
            {
                iapWindowDisplay.SetActive(false);
            }
        }
        else
        {
            bool successfulBuy = false;
            if (productIdEnum == ProductIdEnum.Banana2)
            {
                successfulBuy = iapManager.Buy100Bananas(this);
            }
            else if (productIdEnum == ProductIdEnum.Banana3)
            {
                successfulBuy = iapManager.Buy250Bananas(this);
            }
            else if (productIdEnum == ProductIdEnum.Banana4)
            {
                successfulBuy = iapManager.Buy500Bananas(this);
            }
            else if (productIdEnum == ProductIdEnum.Banana5)
            {
                successfulBuy = iapManager.Buy1000Bananas(this);
            }
            else if (productIdEnum == ProductIdEnum.Banana6)
            {
                successfulBuy = iapManager.Buy2000Bananas(this);
            }
            
            if (successfulBuy)
            {
                menuAudioManager.PlayBuyButtonClick();
                iapWindowDisplay.SetActive(false);
            }
        }
    }

	private bool ShowAd()
    {

        if (Advertisement.IsReady())
        {
            Debug.Log("Ad is ready");
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
            return true;
        }
        else
        {
            Debug.Log("Ad not ready");
            return false;
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                HandleAdFinishedResult();
                break;
            case ShowResult.Skipped:
                HandleAdSkippedResult();
                break;
            case ShowResult.Failed:
                HandleAdFailedResult();
                break;

        }
    }

    private void HandleAdFinishedResult()
    {
        Debug.Log("Ad has finished");
        iapWindowDisplay.SetActive(false);
        AdUtil.WatchAd();
        IncreaseBananaCount(bananaCount);
    }

    private void HandleAdSkippedResult()
    {
        Debug.Log("Ad has been skipped");
    }

    private void HandleAdFailedResult()
    {
        Debug.Log("Ad has failed for some reason");
    }

    public void IncreaseBananaCount(int value)
    {

        Debug.Log("Banana count increase from IAP: "+ value);
        GameProgressionUtil.ChangeBananaCountBy(value);
        progressStorePage.UpdateBananaCount();
    }
}
