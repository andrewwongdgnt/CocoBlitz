using UnityEngine;
using System.Collections;

public class SettingsUtil
{
    
    private readonly static string MUSIC_VOLUME_KEY = "MusicVolumeKey";

    public static void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value);
    }
    public static float GetMusicVolume()
    {
        return PlayerPrefs.HasKey(MUSIC_VOLUME_KEY) ? PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY) : 100;
    }

    private readonly static string SFX_VOLUME_KEY = "SFXVolumeKey";

    public static void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, value);
    }
    public static float GetSFXVolume()
    {
        return PlayerPrefs.HasKey(SFX_VOLUME_KEY) ? PlayerPrefs.GetFloat(SFX_VOLUME_KEY) : 100;
    }

    private readonly static string CARD_DELAY_KEY = "CardDelayKey";

    public static void SetCardDelay(float value)
    {
        PlayerPrefs.SetFloat(CARD_DELAY_KEY, value);
    }
    public static float GetCardDelay()
    {
        return PlayerPrefs.HasKey(CARD_DELAY_KEY) ? PlayerPrefs.GetFloat(CARD_DELAY_KEY) : 1.5f;
    }

}
