using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameProgressionUtil {


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

    public static GameProgressionRepresentation GetGameProgressionRepresentation()
    {
        string stringRep = PlayerPrefs.GetString(GAME_PROGRESSION_KEY);
        GameProgressionRepresentation rep = JsonUtility.FromJson<GameProgressionRepresentation>(stringRep);
        return rep != null ? rep : new GameProgressionRepresentation();
    }

    //Unlock Kongo: Play 50 games
    private readonly static int KONGO_UNLOCK_VALUE = 50;
    public static void IncrementTotalGamesPlayed()
    {
        GameProgressionRepresentation rep = GetGameProgressionRepresentation();
        rep.totalGamesPlayed++;
        SetGameProgressionRepToPlayerPrefs(rep);

        if (rep.totalGamesPlayed >= KONGO_UNLOCK_VALUE)
        {
            UnlockCpu(Cpu.KONGO); 
        }
    }

    //Unlock Purple Monkey: Spend 10 mins playing
    private readonly static float PURPLE_MONKEY_UNLOCK_VALUE = 600f;
    public static void IncreaseTimeSpent(float value)
    {
        GameProgressionRepresentation rep = GetGameProgressionRepresentation();
        rep.totalTimeSpentPlaying+= value;
        SetGameProgressionRepToPlayerPrefs(rep);
        if (rep.totalTimeSpentPlaying >= PURPLE_MONKEY_UNLOCK_VALUE)
        {
            UnlockCpu(Cpu.PURPLE_MONKEY);
        }
    }


    //Unlock Coco: Guess Coco correctly 100 times
    private readonly static float COCO_UNLOCK_VALUE = 100;
    public static void IncrementTotalCountOfWhenCocoWasCorrectlyPicked()
    {
        GameProgressionRepresentation rep = GetGameProgressionRepresentation();
        rep.totalCountOfWhenCocoWasCorrectlyPicked ++;
        SetGameProgressionRepToPlayerPrefs(rep);
        if (rep.totalCountOfWhenCocoWasCorrectlyPicked>= COCO_UNLOCK_VALUE)
        {
            UnlockCpu(Cpu.COCO);
        }
    }
    
    //Unlock Chomp: See chomp 100 times
    private readonly static float CHOMP_UNLOCK_VALUE = 100;
    public static void IncrementTotalCountOfWhenChompWasSeen()
    {
        GameProgressionRepresentation rep = GetGameProgressionRepresentation();
        rep.totalCountOfWhenChompWasSeen ++;
        SetGameProgressionRepToPlayerPrefs(rep);
        if (rep.totalCountOfWhenChompWasSeen >= CHOMP_UNLOCK_VALUE)
        {
            UnlockCpu(Cpu.CHOMP);
        }
    }
    //Unlock Muffin: Guess correctly under 1 second 25 times
    private readonly static float MUFFIN_UNLOCK_VALUE = 25;
    public static void IncrementTotalCountOfCorrectGuessUnderOneSecond()
    {
        GameProgressionRepresentation rep = GetGameProgressionRepresentation();
        rep.totalCountOfCorrectGuessUnderOneSecond ++;
        SetGameProgressionRepToPlayerPrefs(rep);
        if (rep.totalCountOfCorrectGuessUnderOneSecond >= MUFFIN_UNLOCK_VALUE)
        {
            UnlockCpu(Cpu.MUFFIN);
        }
    }

    private static void SetGameProgressionRepToPlayerPrefs(GameProgressionRepresentation rep)
    {
        string json = JsonUtility.ToJson(rep);
        PlayerPrefs.SetString(GAME_PROGRESSION_KEY, json);
    }




}
