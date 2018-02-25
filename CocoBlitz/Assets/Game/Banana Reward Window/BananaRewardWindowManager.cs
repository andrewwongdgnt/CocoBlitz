using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaRewardWindowManager : MonoBehaviour {

    private class BananRewardInfo
    {
        public string reasonString;
        public RewardAndBarrier.Container rb;

        public BananRewardInfo(RewardAndBarrier.Container rb, string reasonString)
        {
            this.rb = rb;
            this.reasonString = reasonString;
        }
    }

    private Stack<BananRewardInfo> bananRewardInfoStack = new Stack<BananRewardInfo>();
    public BananaRewardWindow bananaRewardWindow;
	// Use this for initialization
	void Start () {
        bananRewardInfoStack.Clear();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Unlock()
    {
        AttemptUnlock();
    }

    public void AddRewardToUnlock(RewardAndBarrier.Container rb, string reasonString)
    {
        bananRewardInfoStack.Push(new BananRewardInfo(rb, string.Format(reasonString, rb.Barrier)));
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
        if (bananRewardInfoStack.Count > 0)
        {
            bananaRewardWindow.Show();
            BananRewardInfo bananRewardInfo = bananRewardInfoStack.Pop();

            bananaRewardWindow.SetReason(bananRewardInfo.reasonString);
            bananaRewardWindow.SetReward("x"+bananRewardInfo.rb.Reward);

        } else
        {
            bananaRewardWindow.Hide();
        }
    }
}
