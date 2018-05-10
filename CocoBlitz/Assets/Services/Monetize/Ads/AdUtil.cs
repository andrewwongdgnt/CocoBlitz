using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdUtil {

    private readonly static string TIME_OF_WATCH = "timeOfWatch";

    private static System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);


    public static int GetNextTimeToWatch()
    {
        int curTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;

        int lastTimeSinceWatch = PlayerPrefs.GetInt(TIME_OF_WATCH, 0);


        int diff = curTime - lastTimeSinceWatch;
        //less than 6 hours
        if (diff < 21600)
        {
            return lastTimeSinceWatch + 21600 - curTime;
        }
        else
        {            
            return 0;
        }
    }

    public static void WatchAd()
    {
        int curTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        PlayerPrefs.SetInt(TIME_OF_WATCH, curTime);
    }
}
