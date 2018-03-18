using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainButton : MonoBehaviour {


    public GameUtil.GameModeEnum gameMode;
    public MainPage mainMenu;
    public MenuAudioManager menuAudioManager;

    public void Play()
    {
        menuAudioManager.PlayMainButtonClick();
         mainMenu.SelectGameMode(gameMode);
    }
    

}
