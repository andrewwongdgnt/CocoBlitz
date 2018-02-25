using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressContent : MonoBehaviour {

    public ProgressBar gamesPlayedProgressBar;

	// Use this for initialization
	void Start () {
        
        float currentTotalGamesPlayed = GameProgressionUtil.GetGameProgressionField(rep => rep.totalGamesPlayed);
        RewardAndBarrier.Container nextRB = GameProgressionUtil.GetNextRewardBarrier(RewardAndBarrier.TOTAL_GAMES_PLAYED_PROGRESSION.RewardAndBarriers, currentTotalGamesPlayed);
        gamesPlayedProgressBar.SetValue(currentTotalGamesPlayed, nextRB.Barrier);
        gamesPlayedProgressBar.SetNextReward(nextRB.Reward);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
