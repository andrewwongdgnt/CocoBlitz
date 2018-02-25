using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameProgressionRepresentation {

    //everything is a float to make it easier for calculating rewards and barriers
    public float totalGamesPlayed;

    public float totalSinglePlayerGamesPlayed;

    public float totalTwoPlayersGamesPlayed;

    public float totalGamesWithCpuPlayed;

    public float totalCorrectGuesses;

    //Player has to see to the end of the game for this to count
    public float totalTimeSpentPlaying;

    public float totalCountOfWhenCocoWasCorrectlyPicked;

    public float totalCountOfWhenChompWasSeen;

    public float totalCountOfCorrectGuessUnderTwoSecond;

    public float totalCountOfCorrectGuessUnderOneSecond;
}
