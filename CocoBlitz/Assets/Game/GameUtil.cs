using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil  {

    public enum GameModeEnum { FastestTime, RackUpThePoints };

    public static GameModeEnum currentGameMode = GameModeEnum.RackUpThePoints;
    public static float timer = 5;
    public static int pointsToReach = 5;

    public static List<Cpu> cpuList = new List<Cpu>();
}
