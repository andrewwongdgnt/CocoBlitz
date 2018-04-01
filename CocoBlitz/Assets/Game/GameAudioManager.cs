using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : AudioManager
{


    //-----------------------
    // Guess Button
    //-----------------------

    public AudioClip correct;
    public AudioClip incorrect;
    public void PlayCorrectGuess()
    {
        AudioUtil.PlaySFX(sfxSource,correct);
    }

    public void PlayIncorrectGuess()
    {
        AudioUtil.PlaySFX(sfxSource,incorrect);
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
    // Card Flip
    //-----------------------

    public AudioClip cardFlip;
    public void PlayCardFlip()
    {
        AudioUtil.PlaySFX(sfxSource, cardFlip);
    }


    //-----------------------
    // End Game
    //-----------------------

    public AudioClip reward;
    public void PlayReward()
    {
        AudioUtil.PlaySFX(sfxSource, reward);
    }

}
