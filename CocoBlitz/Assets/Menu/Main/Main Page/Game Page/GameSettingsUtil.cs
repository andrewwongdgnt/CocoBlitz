using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
