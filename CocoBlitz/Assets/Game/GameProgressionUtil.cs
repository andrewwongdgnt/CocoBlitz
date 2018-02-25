using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameProgressionUtil {

    private readonly static string BANANA_COUNT_KEY = "BananaCount";

    public static bool ChangeBananaCountBy(int value)
    {
        int bananaCount = GetBananas();
        if (bananaCount+ value >= 0)
        {
            PlayerPrefs.SetInt(BANANA_COUNT_KEY, bananaCount + value);
            return true;
        }
        return false;
    }

    public static int GetBananas()
    {
        return PlayerPrefs.GetInt(BANANA_COUNT_KEY, 0);
    }

    private readonly static string CPU_AVAILABILITY_KEY = "CpuAvailabilityKey";

    public static void UnlockCpu(Cpu cpu)
    {
        if (cpu.starter)
        {
            return;
        }

        HashSet<string> unlockedCpus = GetUnlockedCpus();
        if (unlockedCpus.Contains(cpu.name))
        {
            //Already unlocked
            return;
        }
        unlockedCpus.Add(cpu.name);
        string newCommaSeperatedListOfCpus = string.Join(",", unlockedCpus.ToArray<string>());
        PlayerPrefs.SetString(CPU_AVAILABILITY_KEY, newCommaSeperatedListOfCpus);

    }
    public static bool GetCpuAvailability(Cpu cpu)
    {
        if (cpu.starter)
        {
            return true;
        }

        HashSet<string> unlockedCpus = GetUnlockedCpus();
        return unlockedCpus.Contains(cpu.name);
    }

    private static HashSet<string> GetUnlockedCpus()
    {
        string commaSeperatedListOfCpus = PlayerPrefs.GetString(CPU_AVAILABILITY_KEY);
        return  new HashSet<string>(commaSeperatedListOfCpus.Split(',').Select(c => c).ToList());
    }
    
    private readonly static string GAME_PROGRESSION_KEY = "GameProgressionKey";


    public static RewardAndBarrier.Container[] GetCorrectRewards(RewardAndBarrier.Container[] rewardAndBarriers, float initValue, float finalBarrier)
    {
        return Array.FindAll(rewardAndBarriers, c => c.Barrier >= initValue && c.Barrier <= finalBarrier);
    }

   
    public static RewardAndBarrier.Container GetNextRewardBarrier(RewardAndBarrier.Container[] rewardAndBarriers, float currentValue)
    {
        RewardAndBarrier.Container rb = rewardAndBarriers[0];
        for (int i=0; i< rewardAndBarriers.Length; i++)
        {
            rb = rewardAndBarriers[i];
            if (currentValue<rb.Barrier)
            {
                break;
            }
        }
        return rb;
    }

    public static float GetGameProgressionField(Func<GameProgressionRepresentation, float> getRepField)
    {
        GameProgressionRepresentation rep = GetGameProgressionRepresentation();
        return getRepField(rep);
    }
    public static void UpdateGameProgression(Action<GameProgressionRepresentation> updateRep)
    {
        GameProgressionRepresentation rep = GetGameProgressionRepresentation();
        updateRep(rep);
        SetGameProgressionRepToPlayerPrefs(rep);
    }
    private static GameProgressionRepresentation GetGameProgressionRepresentation()
    {
        string stringRep = PlayerPrefs.GetString(GAME_PROGRESSION_KEY);
        GameProgressionRepresentation rep = JsonUtility.FromJson<GameProgressionRepresentation>(stringRep);
        return rep != null ? rep : new GameProgressionRepresentation();
    }

    private static void SetGameProgressionRepToPlayerPrefs(GameProgressionRepresentation rep)
    {
        string json = JsonUtility.ToJson(rep);
        PlayerPrefs.SetString(GAME_PROGRESSION_KEY, json);
    }




}
