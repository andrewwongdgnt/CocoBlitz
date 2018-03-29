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

    public static void PlayMusic(AudioSource audioSource, AudioClip audioClip,  AudioClip audioClip2=null, MonoBehaviour mb=null)
    {
        if (audioSource == null || audioClip == null)
            return;
        audioSource.volume = SettingsUtil.GetMusicVolume() / 100;
        audioSource.loop = audioClip2==null;
        PlayAudio(audioSource, audioClip);

        if (audioClip2 != null)
        {
            mb.StartCoroutine(WaitForMusic1ToEnd(audioSource, audioClip2));
        }
    }

    public static IEnumerator WaitForMusic1ToEnd(AudioSource audioSource, AudioClip audioClip2)
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        // or yield return new WaitWhile(() => audiosource.isPlaying == true);
        audioSource.loop = true;
        PlayAudio(audioSource, audioClip2);
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
