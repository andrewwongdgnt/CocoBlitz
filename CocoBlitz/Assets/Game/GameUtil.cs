using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class GameUtil  {

    public enum GameModeEnum { Coco, GoGo };

    public static GameModeEnum currentGameMode = GameModeEnum.GoGo;
    public static float timer = 5;
    public static int pointsToReach = 5;

    public static List<Cpu> cpuList = new List<Cpu>();


    private readonly static string CPU_AVAILABILITY_KEY = "CpuAvailabilityKey";

    public static void UnlockCpu(Cpu cpu)
    {
        if (cpu.starter)
        {
            return;
        }

        string jsonString = PlayerPrefs.GetString(CPU_AVAILABILITY_KEY);
        JSONNode node = JSON.Parse(jsonString);
        if (node == null)
        {
            node = new JSONClass();
        }
        node[cpu.name].AsBool = true;
        PlayerPrefs.SetString(CPU_AVAILABILITY_KEY, node.ToString());

    }
    public static bool GetCpuAvailability(Cpu cpu)
    {
        if (cpu.starter)
        {
            return true;
        }

        string jsonString=PlayerPrefs.GetString(CPU_AVAILABILITY_KEY);
        JSONNode node = JSON.Parse(jsonString);
        if (node != null)
        {
            return node[cpu.name].AsBool;
        }
        return false;
    }
}
