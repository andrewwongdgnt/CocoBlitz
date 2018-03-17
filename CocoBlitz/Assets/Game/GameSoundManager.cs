using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip correct;
    public AudioClip incorrect;

    //-----------------------
    // Guess Button
    //-----------------------

    public void PlayCorrectGuess()
    {
        PlaySFX(correct);
    }

    public void PlayIncorrectGuess()
    {
        PlaySFX(incorrect);
    }


    private void PlaySFX(AudioClip audioClip)
    {
        if (sfxSource == null || audioClip == null)
            return;
        sfxSource.volume = SettingsUtil.GetSFXVolume() / 100;
        sfxSource.loop = false;
        PlayAudio(sfxSource, audioClip);
    }

    private void PlayMusic(AudioClip audioClip)
    {
        if (musicSource == null || audioClip == null)
            return;
        musicSource.volume = SettingsUtil.GetMusicVolume() / 100;
        musicSource.loop = true;
        PlayAudio(musicSource, audioClip);
    }

    private void PlayAudio(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
