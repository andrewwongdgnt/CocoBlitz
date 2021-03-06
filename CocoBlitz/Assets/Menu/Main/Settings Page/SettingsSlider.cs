﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour {

    public MenuAudioManager menuAudioManager;
    public Text valueText;
    public SettingsPage.SettingsEnum settings;
    public SettingsPage settingsPage;
    public void SetSettings(float value)
    {

        float finalValue = GetFinalValue(value); 
        valueText.text = GetFinalValueString(finalValue);
        settingsPage.SetSettings(settings, finalValue);
        if (settings == SettingsPage.SettingsEnum.Sound || settings == SettingsPage.SettingsEnum.CardDelay)
        {
            menuAudioManager.PlaySliderOnValueChanged();
        }
        else if (settings == SettingsPage.SettingsEnum.Music)
        {
            menuAudioManager.UpdateMusicVolume();
        }
    }

    private string GetFinalValueString(float v)
    {
        if (settings == SettingsPage.SettingsEnum.CardDelay)
        {
           return v.ToString("0.00")+" s";
        }
        return  v.ToString(); 
    }

    private float GetFinalValue(float value)
    {
        return settings == SettingsPage.SettingsEnum.CardDelay ? value / 4 : value;
    }
}
