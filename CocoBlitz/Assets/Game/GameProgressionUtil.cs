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


    public enum BuyableCpuEnum { Kongo, PurpleMonkey, Coco, Muffin, Chomp };

    public static Dictionary<BuyableCpuEnum, int> CPU_COST_MAP = new Dictionary<BuyableCpuEnum, int>()
    {
        { BuyableCpuEnum.Kongo, 100},
        { BuyableCpuEnum.PurpleMonkey, 100},
        { BuyableCpuEnum.Coco, 100},
        { BuyableCpuEnum.Muffin, 100},
        { BuyableCpuEnum.Chomp, 100},
    };


    public static Dictionary<BuyableCpuEnum, Cpu> CPU_MAP = new Dictionary<BuyableCpuEnum, Cpu>()
    {
        { BuyableCpuEnum.Kongo, Cpu.KONGO},
        { BuyableCpuEnum.PurpleMonkey, Cpu.PURPLE_MONKEY},
        { BuyableCpuEnum.Coco, Cpu.COCO},
        { BuyableCpuEnum.Muffin, Cpu.MUFFIN},
        { BuyableCpuEnum.Chomp, Cpu.CHOMP},
    };

    public enum BuyStatus { NOT_ENOUGH_BANANAS, ALREADY_BOUGHT, SUCCESSFUL };
    public static BuyStatus BuyCpu(BuyableCpuEnum cpuToBuy)
    {
        //check if cpu already unlocked
        if (GetCpuAvailability(CPU_MAP[cpuToBuy]))
        {
            return BuyStatus.ALREADY_BOUGHT;
        }

        bool boughtSuccessful = ChangeBananaCountBy(-CPU_COST_MAP[cpuToBuy]);
        if (boughtSuccessful)
        {
            UnlockCpu(CPU_MAP[cpuToBuy]);
            return BuyStatus.SUCCESSFUL;
        } else
        {
            return BuyStatus.NOT_ENOUGH_BANANAS;
        }
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


    public static RewardAndBarrier[] GetCorrectRewards(RewardAndBarrier[] rewardAndBarriers, float initValue, float finalBarrier)
    {
        return Array.FindAll(rewardAndBarriers, c => c.Barrier > initValue && c.Barrier <= finalBarrier);
    }    
    
    public static RewardAndBarrier GetNextRewardBarrier(RewardAndBarrier[] rewardAndBarriers, float currentValue)
    {
        
        for (int i=0; i< rewardAndBarriers.Length; i++)
        {
            if (currentValue< rewardAndBarriers[i].Barrier)
            {
                return rewardAndBarriers[i];
                
            }
        }
        return new RewardAndBarrier(rewardAndBarriers[rewardAndBarriers.Length - 1].Barrier, 0);
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
