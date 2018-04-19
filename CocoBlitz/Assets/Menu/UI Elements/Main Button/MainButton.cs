using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainButton : MonoBehaviour {


    public MainPage mainMenu;
    public MenuAudioManager menuAudioManager;

    public void Play()
    {
        menuAudioManager.PlayMainButtonClick();
         mainMenu.PlayOffline();
    }
    

}
