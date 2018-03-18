using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;

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
