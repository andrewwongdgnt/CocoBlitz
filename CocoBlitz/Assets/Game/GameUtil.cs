using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUtil  {

    public enum GameModeEnum { Coco, GoGo };

    public static GameModeEnum currentGameMode = GameModeEnum.GoGo;
    public static float timer = 620;
    public static int pointsToReach = 5;

    public static List<Cpu> cpuList = new List<Cpu>();


}
