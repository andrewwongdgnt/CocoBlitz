using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;

using UnityEngine;
using System;
using UnityEngine.UI;

public class BuyBanana : MonoBehaviour {

    public int costInCents;
    public Text costInCentsTxt;
    public int bananaCount;
    public Text bananaCountTxt;
    public ProgressStorePage progressStorePage;
    public IAPWindowDisplay iapWindowDisplay;
    public MenuAudioManager menuAudioManager;

    void Start()
    {
        if  (costInCents > 0) {

            float costInDollar = costInCents / 100f;
            costInCentsTxt.text = "$"+costInDollar.ToString();
        }


        bananaCountTxt.text = bananaCount.ToString();
    }

    void Update()
    {
        if (costInCents == 0)
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
        if (costInCents == 0) {
            if (AdUtil.GetNextTimeToWatch() == 0)
            {
                menuAudioManager.PlayMainButtonClick();
                ShowAd();
            }
        }
        else
        {

        }
    }

	private void ShowAd()
    {

        if (Advertisement.IsReady())
        {
            Debug.Log("Ad is ready");
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
        else
        {
            Debug.Log("Ad not ready");
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

    private void IncreaseBananaCount(int value)
    {

        Debug.Log("Banana count increase from IAP");
        GameProgressionUtil.ChangeBananaCountBy(value);
        progressStorePage.UpdateBananaCount();
    }
}
