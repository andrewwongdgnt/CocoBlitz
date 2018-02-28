using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameProgressionRepresentation {

    //everything is a float to make it easier for calculating rewards and barriers
    public float totalSinglePlayerGamesPlayed;

    public float totalTwoPlayerGamesPlayed;

    //Player has to see to the end of the game for this to count
    public float totalTimeSpentPlaying;

    public float totalCpusDefeated;

    public float totalCorrectCorrectlyColoredGuesses;

    public float totalCorrectIncorrectlyColoredGuesses;

    public float totalCorrectGuessesUnderOneSecond;

    public float totalTimesYellowBananaWasSeen;

    public float totalTimesCocoWasPicked;

    public float totalTimesChompWasPicked;
    

    //Secrets. First achievement is unknown.  Then after that, it is known.
    public float totalCorrectGuessesUnderHalfASecond;

    public float totalGamesWithAndrewAndKelsey;

    public float totalGamesWithCocoAndMuffin;

    public float totalGamesWithMonkeyAndPenguin;

    public float totalCorrectGuessesStreak;

}
