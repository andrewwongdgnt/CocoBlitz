using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    public AudioSource sfxSource;
    public AudioSource musicSource;

    void Start()
    {
        PlayBackgroundMusic();
    }

    //-----------------------
    // Background Music
    //-----------------------

    public Music[] backgroundMusicList;
    public void PlayBackgroundMusic()
    {
        Music gameBackgroundMusic = backgroundMusicList[UnityEngine.Random.Range(0, backgroundMusicList.Length)];
        AudioClip music1 = gameBackgroundMusic.music1;
        AudioClip music2 = gameBackgroundMusic.music2;

        AudioUtil.PlayMusic(musicSource, music1, music2, music2 == null ? null : this);
    }

    public void FadeOutBackgroundMusic()
    {
        StartCoroutine(FadeOutBackgroundMusicInternal());
    }

    private IEnumerator FadeOutBackgroundMusicInternal()
    {

        while (musicSource.volume > 0)
        {
            musicSource.volume -= 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }

        musicSource.volume = 0;

    }

    public void FadeInBackgroundMusic()
    {
        StartCoroutine(FadeInBackgroundMusicInternal());
    }

    private IEnumerator FadeInBackgroundMusicInternal()
    {

        while (musicSource.volume < SettingsUtil.GetMusicVolume() / 100)
        {
            musicSource.volume += 0.1f;

            yield return new WaitForSecondsRealtime(0.1f);
        }

        musicSource.volume = SettingsUtil.GetMusicVolume() / 100;
    }


    //-----------------------
    // Voices
    //-----------------------
    public void PlayAVoice(AudioClip clip)
    {
        AudioUtil.PlaySFX(sfxSource, clip);
    }
}
