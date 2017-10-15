﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager  {

    public enum GameModeEnum { FastestTime, RackUpThePoints, AgainstTheCPUs };

    public static GameModeEnum currentGameMode = GameModeEnum.FastestTime;
    public static float timer = 10;
    public static int pointsToReach = 5;
}