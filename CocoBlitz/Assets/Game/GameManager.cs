using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager  {

    public enum GameModeEnum { FastestTime, RackUpThePoints, AgainstTheCPUs };

    public static GameModeEnum currentGameMode = GameModeEnum.RackUpThePoints;
    public static float timer = 10;
    public static int pointsToReach = 10;
}
