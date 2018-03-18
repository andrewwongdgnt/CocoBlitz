using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUtil  {

    public static void PlaySFX(AudioSource audioSource, AudioClip audioClip, bool forcePlay=true)
    {
        if (audioSource == null || audioClip == null)
            return;
        audioSource.volume = SettingsUtil.GetSFXVolume() / 100;
        audioSource.loop = false;
        PlayAudio(audioSource, audioClip, forcePlay);
    }

    public static void PlayMusic(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource == null || audioClip == null)
            return;
        audioSource.volume = SettingsUtil.GetMusicVolume() / 100;
        audioSource.loop = true;
        PlayAudio(audioSource, audioClip);
    }

    private static void PlayAudio(AudioSource audioSource, AudioClip audioClip, bool forcePlay = true)
    {
        if (audioSource == null || audioClip == null)
            return;
        audioSource.clip = audioClip;
        if (forcePlay || !audioSource.isPlaying)
            audioSource.Play();
    }

}
