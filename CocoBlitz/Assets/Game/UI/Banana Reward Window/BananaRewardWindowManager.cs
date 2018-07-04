using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaRewardWindowManager : MonoBehaviour {

    private class BananRewardInfo
    {
        public string reasonString;
        public RewardAndBarrier rb;

        public BananRewardInfo(RewardAndBarrier rb, string reasonString)
        {
            this.rb = rb;
            this.reasonString = reasonString;
        }
    }
    public GameAudioManager gameAudioManager;
    private Stack<BananRewardInfo> bananaRewardInfoStack = new Stack<BananRewardInfo>();
    public BananaRewardWindow bananaRewardWindow;

	
	// Update is called once per frame
	void Update () {
		
	}

    public void Unlock()
    {
        AttemptUnlock(false);
    }
    public void UnlockAll()
    {
        bananaRewardInfoStack.Clear();
        bananaRewardWindow.Hide();
    }

    public void AddRewardToUnlock(RewardAndBarrier rb, string reasonString)
    {
        bananaRewardInfoStack.Push(new BananRewardInfo(rb, string.Format(reasonString, rb.Barrier)));
    }

    public void BeginUnlockPhase()
    {
        Show();
        bool windowShowing = AttemptUnlock(true);

        if (!windowShowing)
        {
            gameAudioManager.PlayCheer();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {

        gameObject.SetActive(true);
    }

    private bool AttemptUnlock(bool playRewardSound)
    {
        if (bananaRewardInfoStack.Count > 0)
        {
            if (playRewardSound)
                gameAudioManager.PlayReward();
            bananaRewardWindow.Show(bananaRewardInfoStack.Count);
            BananRewardInfo bananRewardInfo = bananaRewardInfoStack.Pop();

            bananaRewardWindow.SetReason(bananRewardInfo.reasonString);
            bananaRewardWindow.SetReward("x"+bananRewardInfo.rb.Reward);

            return true;

        } else
        {
            bananaRewardWindow.Hide();
            return false;
        }
    }
}
