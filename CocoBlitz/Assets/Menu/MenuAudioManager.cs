using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : AudioManager {





    public void UpdateMusicVolume()
    {
        musicSource.volume = SettingsUtil.GetMusicVolume() / 100;
    }

    //-----------------------
    // Main Button
    //-----------------------
    public AudioClip mainButtonClick;
    public void PlayMainButtonClick()
    {
        AudioUtil.PlaySFX(sfxSource, mainButtonClick);
    }

    //-----------------------
    // Nav Button
    //-----------------------

    public AudioClip navButtonClick;
    public void PlayNavButtonClick()
    {
        AudioUtil.PlaySFX(sfxSource, navButtonClick);
    }

    //-----------------------
    // Buy Button
    //-----------------------

    public AudioClip buyButtonClick;
    public void PlayBuyButtonClick()
    {
        AudioUtil.PlaySFX(sfxSource, buyButtonClick);
    }
    public AudioClip buyButtonNotEnoughClick;
    public void PlayBuyButtonNotEnoughClick()
    {
        AudioUtil.PlaySFX(sfxSource, buyButtonNotEnoughClick);
    }

    //-----------------------
    // Toggle
    //-----------------------

    public AudioClip toggleClick;
    public void PlayToggleClick()
    {
        AudioUtil.PlaySFX(sfxSource, toggleClick);
    }

    //-----------------------
    // Slider
    //-----------------------

    public AudioClip sliderOnValueChanged;
    public void PlaySliderOnValueChanged()
    {
        AudioUtil.PlaySFX(sfxSource, sliderOnValueChanged,false);
    }
}
