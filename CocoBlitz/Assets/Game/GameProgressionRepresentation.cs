using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameProgressionRepresentation {

    public int totalGamesPlayed;

    //Player has to see to the end of the game for this to count
    public float totalTimeSpentPlaying;

    public int totalCountOfWhenCocoWasCorrectlyPicked;

    public int totalCountOfWhenChompWasSeen;

    public int totalCountOfCorrectGuessUnderOneSecond;
}
