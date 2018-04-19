using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameSettingsUtil  {


    public enum GameModeEnum { Coco, GoGo };

    private readonly static string GAME_MODE_KEY = "GameModeKey";

    public static void SetGameMode(GameModeEnum value)
    {
        PlayerPrefs.SetString(GAME_MODE_KEY, value.ToString());
    }
    public static GameModeEnum GetGameMode()
    {
        GameModeEnum ret;

        string gameModeString = PlayerPrefs.GetString(GAME_MODE_KEY, GameModeEnum.Coco.ToString());

        try
        {
            ret = (GameModeEnum)Enum.Parse(typeof(GameModeEnum), gameModeString);
        }
        catch (Exception ex)
        {
            Debug.Log("Could not parse GameModeEnum: "+  ex.Message);
            ret = GameModeEnum.Coco;
        }
        return ret;
    }

    private readonly static string COCO_MODE_POINTS_TO_REACH_KEY = "CocoModePointsToReachKey";

    public static void SetCocoModePointsToReach(int value)
    {
        PlayerPrefs.SetInt(COCO_MODE_POINTS_TO_REACH_KEY, value);
    }
    public static int GetCocoModePointsToReach()
    {
        return PlayerPrefs.GetInt(COCO_MODE_POINTS_TO_REACH_KEY,10);
    }

    private readonly static string GO_GO_MODE_TIMER_KEY = "GoGoModeTimerKey";

    public static void SetGoGoModeTimer(float value)
    {
        PlayerPrefs.SetFloat(GO_GO_MODE_TIMER_KEY, value);
    }
    public static float GetGoGoModeTimer()
    {
        return PlayerPrefs.GetFloat(GO_GO_MODE_TIMER_KEY,10);
    }


    private readonly static string CPU_IN_PLAY_KEY = "CpuInPlayKey";

    public readonly static int MAX_CPU_IN_PLAY_COUNT = 3;
    public readonly static int TOTAL_CPUS_IN_EXISTENCE = 10;

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
    
    public enum GameTypeEnum { Single, Two };
    private readonly static string GAME_TYPE_KEY = "GameTypeKey";

    public static void SetGameType(GameTypeEnum value)
    {
        PlayerPrefs.SetString(GAME_TYPE_KEY, value.ToString());
    }
    public static GameTypeEnum GetGameType()
    {

        GameTypeEnum ret;

        string gameTypeString = PlayerPrefs.GetString(GAME_TYPE_KEY, GameTypeEnum.Single.ToString());

        try
        {
            ret = (GameTypeEnum)Enum.Parse(typeof(GameTypeEnum), gameTypeString);
        }
        catch (Exception ex)
        {
            Debug.Log("Could not parse GameTypeEnum: " + ex.Message);
            ret = GameTypeEnum.Single;
        }
        return ret;
    }
}
