using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;

using UnityEngine;
using System;

public class PlayAd : MonoBehaviour {

	public void ShowAd()
    {
        Debug.Log("is ad init? " + Advertisement.isInitialized);
        Debug.Log("game id: " + Advertisement.gameId);

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
    }

    private void HandleAdSkippedResult()
    {
        Debug.Log("Ad has been skipped");
    }

    private void HandleAdFailedResult()
    {
        Debug.Log("Ad has failed for some reason");
    }
}
