using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;


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


}
