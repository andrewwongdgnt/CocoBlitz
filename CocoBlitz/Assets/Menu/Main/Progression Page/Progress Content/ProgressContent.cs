using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressContent : MonoBehaviour {

    public ProgressBar singlePlayerGamesPlayedProgressBar;
    public ProgressBar twoPlayerGamesPlayedProgressBar;
    public ProgressBar timeSpentProgressBar;
    public ProgressBar cpusDefeatedProgressBar;
    public ProgressBar correctCorrectlyColoredProgressBar;
    public ProgressBar correctIncorrectlyColoredProgressBar;
    public ProgressBar correctGuessesUnderOneSecondProgressBar;
    public ProgressBar yellowBananasSeenProgressBar;
    public ProgressBar cocoPickedProgressBar;
    public ProgressBar chompPickedProgressBar;

    public ProgressBar correctGuessesUnderHalfSecondProgressBar;
    public ProgressBar gamesPlayedWithAndrewAndKelseyProgressBar;
    public ProgressBar gamesPlayedWithCocoAndMuffinProgressBar;
    public ProgressBar gamesPlayedWithMonkeyAndPenguinProgressBar;
    public ProgressBar correctGuessesStreakProgressBar;

    // Use this for initialization
    void Start () {

        SetUpProgressBar(singlePlayerGamesPlayedProgressBar, rep => rep.totalSinglePlayerGamesPlayed, RewardAndBarrier.TOTAL_SINGLE_PLAYER_GAMES_PLAYED_PROGRESSION);
        SetUpProgressBar(twoPlayerGamesPlayedProgressBar, rep => rep.totalTwoPlayerGamesPlayed, RewardAndBarrier.TOTAL_TWO_PLAYER_GAMES_PLAYED_PROGRESSION);
        SetUpProgressBar(timeSpentProgressBar, rep => rep.totalTimeSpentPlaying, RewardAndBarrier.TOTAL_TIME_SPENT_PLAYING_PROGRESSION, value => (float)Math.Floor(value));
        SetUpProgressBar(cpusDefeatedProgressBar, rep => rep.totalCpusDefeated, RewardAndBarrier.TOTAL_CPUS_DEFEATED_PROGRESSION);
        SetUpProgressBar(correctCorrectlyColoredProgressBar, rep => rep.totalCorrectCorrectlyColoredGuesses, RewardAndBarrier.TOTAL_CORRECT_CORRECTLY_COLORED_GUESSES_PROGRESSION);
        SetUpProgressBar(correctIncorrectlyColoredProgressBar, rep => rep.totalCorrectIncorrectlyColoredGuesses, RewardAndBarrier.TOTAL_CORRECT_INCORRECTLY_COLORED_GUESSES_PROGRESSION);
        SetUpProgressBar(correctGuessesUnderOneSecondProgressBar, rep => rep.totalCorrectGuessesUnderOneSecond, RewardAndBarrier.TOTAL_CORRECT_GUESSES_UNDER_ONE_SECOND_PROGRESSION);
        SetUpProgressBar(yellowBananasSeenProgressBar, rep => rep.totalTimesYellowBananaWasSeen, RewardAndBarrier.TOTAL_TIMES_YELLOW_BANANA_WAS_SEEN_PROGRESSION);
        SetUpProgressBar(cocoPickedProgressBar, rep => rep.totalTimesCocoWasPicked, RewardAndBarrier.TOTAL_TIMES_COCO_WAS_PICKED_PROGRESSION);
        SetUpProgressBar(chompPickedProgressBar, rep => rep.totalTimesChompWasPicked, RewardAndBarrier.TOTAL_TIMES_CHOMP_WAS_PICKED_PROGRESSION);

        SetUpProgressBar(correctGuessesUnderHalfSecondProgressBar, rep => rep.totalCorrectGuessesUnderHalfASecond, RewardAndBarrier.TOTAL_CORRECT_GUESSES_UNDER_HALF_A_SECOND_PROGRESSION);
        SetUpProgressBar(gamesPlayedWithAndrewAndKelseyProgressBar, rep => rep.totalGamesWithAndrewAndKelsey, RewardAndBarrier.TOTAL_GAMES_WITH_ANDREW_AND_KELSEY_PROGRESSION);
        SetUpProgressBar(gamesPlayedWithCocoAndMuffinProgressBar, rep => rep.totalGamesWithCocoAndMuffin, RewardAndBarrier.TOTAL_GAMES_WITH_COCO_AND_MUFFIN_PROGRESSION);
        SetUpProgressBar(gamesPlayedWithMonkeyAndPenguinProgressBar, rep => rep.totalGamesWithMonkeyAndPenguin, RewardAndBarrier.TOTAL_GAMES_WITH_MONKEY_AND_PENGUIN_PROGRESSION);
        SetUpProgressBar(correctGuessesStreakProgressBar, rep => rep.totalCorrectGuessesStreak, RewardAndBarrier.TOTAL_CORRECT_GUESSES_STREAK_PROGRESSION);

    }
	
	private void SetUpProgressBar(ProgressBar progressBar, Func<GameProgressionRepresentation, float> getRepField, RewardAndBarrier[] rewardAndBarriers, Func<float,float> valueConverter=null) {

        float progressionField = GameProgressionUtil.GetGameProgressionField(getRepField);
        RewardAndBarrier nextRB = GameProgressionUtil.GetNextRewardBarrier(rewardAndBarriers, progressionField);
        progressBar.SetValue(valueConverter != null ? valueConverter(progressionField) : progressionField, valueConverter!=null ? valueConverter(nextRB.Barrier) : nextRB.Barrier);
        progressBar.SetNextReward(nextRB.Reward);
        progressBar.SetLabelValue(nextRB.Secret);
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }
}
