using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;

    //-----------------------
    // Game Background Music
    //-----------------------

    [System.Serializable]
    public class Music 
    {
        public AudioClip music1;
        public AudioClip music2;
    }

    public Music[] gameBackgroundMusicList;
    public void PlayBackgroundMusic()
    {        
        Music gameBackgroundMusic = gameBackgroundMusicList[UnityEngine.Random.Range(0, gameBackgroundMusicList.Length)];
        AudioClip music1 = gameBackgroundMusic.music1;
        AudioClip music2 = gameBackgroundMusic.music2;

        AudioUtil.PlayMusic(musicSource, music1, music2, music2==null ? null : this);
    }
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
    // Main Button
    //-----------------------

    public AudioClip cardFlip;
    public void PlayCardFlip()
    {
        AudioUtil.PlaySFX(sfxSource, cardFlip);
    }


    //-----------------------
    // End Game
    //-----------------------

    public AudioClip win;
    public void PlayWinGame()
    {
        AudioUtil.PlaySFX(sfxSource, win);
    }

}
