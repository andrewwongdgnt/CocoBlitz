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

    private Stack<BananRewardInfo> bananaRewardInfoStack = new Stack<BananRewardInfo>();
    public BananaRewardWindow bananaRewardWindow;
	// Use this for initialization
	void Start () {
        bananaRewardInfoStack.Clear();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Unlock()
    {
        AttemptUnlock();
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
        AttemptUnlock();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {

        gameObject.SetActive(true);
    }

    private void AttemptUnlock()
    {
        if (bananaRewardInfoStack.Count > 0)
        {
            bananaRewardWindow.Show(bananaRewardInfoStack.Count);
            BananRewardInfo bananRewardInfo = bananaRewardInfoStack.Pop();

            bananaRewardWindow.SetReason(bananRewardInfo.reasonString);
            bananaRewardWindow.SetReward("x"+bananRewardInfo.rb.Reward);

        } else
        {
            bananaRewardWindow.Hide();
        }
    }
}
