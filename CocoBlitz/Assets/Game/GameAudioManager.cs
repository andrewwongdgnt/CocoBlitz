using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip correct;
    public AudioClip incorrect;

    //-----------------------
    // Guess Button
    //-----------------------

    public void PlayCorrectGuess()
    {
        AudioUtil.PlaySFX(sfxSource,correct);
    }

    public void PlayIncorrectGuess()
    {
        AudioUtil.PlaySFX(sfxSource,incorrect);
    }

   
}
