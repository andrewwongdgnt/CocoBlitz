using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPage : MonoBehaviour, Page {

    public enum SettingsEnum { Music, Sound, CardDelay };
    public Slider musicVolume;
    public Slider sfxVolume;
    public Slider cardDelay;

    // Use this for initialization
    void Start ()
    {
        cardDelay.value = SettingsUtil.GetCardDelay()*4;
        musicVolume.value = SettingsUtil.GetMusicVolume();
        sfxVolume.value = SettingsUtil.GetSFXVolume();
    }

    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
    }

    public void SetSettings(SettingsEnum settings, float value)
    {
        if (settings == SettingsEnum.Music)
            MusicVolume(value);
        else if (settings == SettingsEnum.Sound)
            SFXVolume(value);
        else if (settings == SettingsEnum.CardDelay)
            CardDelay(value);
    }

     void MusicVolume(float value)
    {
        SettingsUtil.SetMusicVolume(value);
    }

     void SFXVolume(float value)
    {
        SettingsUtil.SetSFXVolume(value);
    }

    void CardDelay(float value)
    {
        SettingsUtil.SetCardDelay(value);
    }
}
