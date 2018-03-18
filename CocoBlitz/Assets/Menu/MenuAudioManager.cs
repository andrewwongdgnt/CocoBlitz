using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip mainButtonClick;
    public AudioClip navButtonClick;

    //-----------------------
    // Main Button
    //-----------------------

    public void PlayMainButtonClick()
    {
        AudioUtil.PlaySFX(sfxSource, mainButtonClick);
    }

    //-----------------------
    // Option Button
    //-----------------------

    public void PlayNavButtonClick()
    {
        AudioUtil.PlaySFX(sfxSource, navButtonClick);
    }

}
