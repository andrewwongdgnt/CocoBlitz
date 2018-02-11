using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameSettingsUtil  {

    private readonly static string COCO_MODE_POINTS_TO_REACH_KEY = "CocoModePointsToReachKey";

    public static void SetCocoModePointsToReach(int value)
    {
        PlayerPrefs.SetInt(COCO_MODE_POINTS_TO_REACH_KEY, value);
    }
    public static int GetCocoModePointsToReach()
    {
        return PlayerPrefs.HasKey(COCO_MODE_POINTS_TO_REACH_KEY) ? PlayerPrefs.GetInt(COCO_MODE_POINTS_TO_REACH_KEY) : 10;
    }

    private readonly static string GO_GO_MODE_TIMER_KEY = "GoGoModeTimerKey";

    public static void SetGoGoModeTimer(float value)
    {
        PlayerPrefs.SetFloat(GO_GO_MODE_TIMER_KEY, value);
    }
    public static float GetGoGoModeTimer()
    {
        return PlayerPrefs.HasKey(GO_GO_MODE_TIMER_KEY) ? PlayerPrefs.GetFloat(GO_GO_MODE_TIMER_KEY) : 10f;
    }


    private readonly static string CPU_IN_PLAY_KEY = "CpuInPlayKey";

    private readonly static int MAX_CPU_IN_PLAY_COUNT = 3;
    private readonly static int TOTAL_CPUS_IN_EXISTENCE = 10;

    public static void SetCpuInPlay(int cpuIndex, int cpuPickedIndex)
    {
        int resolvedCpuPickedIndex = ResolveCpuIndex(cpuPickedIndex);       

        List<int> cpusInPlay = GetCpusInPlayList();
        cpusInPlay[cpuIndex] = resolvedCpuPickedIndex;
        string commaSeperatedListOfCpusInPlay =  string.Join(",", cpusInPlay.Select(e=> e.ToString()).ToArray());

        PlayerPrefs.SetString(CPU_IN_PLAY_KEY, commaSeperatedListOfCpusInPlay);
    }
    public static List<int> GetCpusInPlayList()
    {
        string commaSeperatedListOfCpusInPlay = PlayerPrefs.GetString(CPU_IN_PLAY_KEY);
        List<int> cpusInPlay = commaSeperatedListOfCpusInPlay.Split(',').Select(e => {

            int value = 0;
            Int32.TryParse(e, out value);

            return ResolveCpuIndex(value);
        }).ToList();
        for (int i = 0; i< MAX_CPU_IN_PLAY_COUNT; i++)
        {
            if (cpusInPlay.Count <= i)
            {
                cpusInPlay.Add(0);
            }
        }

       return cpusInPlay;
    }

    private static int ResolveCpuIndex(int cpuIndex)
    {
        if (cpuIndex < 0)
        {
            return 0;
        }
        else if (cpuIndex >= TOTAL_CPUS_IN_EXISTENCE)
        {
            return TOTAL_CPUS_IN_EXISTENCE - 1;
        }
        return cpuIndex;
    }



    private readonly static string GAME_TYPE_KEY = "GameTypeKey";
    public readonly static string GAME_TYPE_SINGLE_PLAYER = "GameTypeSinglePlayer";
    public readonly static string GAME_TYPE_TWO_PLAYERS = "GameTypeTwoPlayers";

    public static void SetGameTypeKey(string value)
    {
        PlayerPrefs.SetString(GAME_TYPE_KEY, value);
    }
    public static string GetGameTypeKey()
    {
        string gameTypeString = PlayerPrefs.HasKey(GAME_TYPE_KEY) ? PlayerPrefs.GetString(GAME_TYPE_KEY) : GAME_TYPE_SINGLE_PLAYER;
        return gameTypeString == GAME_TYPE_SINGLE_PLAYER || gameTypeString == GAME_TYPE_TWO_PLAYERS ? gameTypeString : GAME_TYPE_SINGLE_PLAYER;
    }
}
