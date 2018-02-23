using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour {

    public GameUtil.GameModeEnum gameMode;
    public MainPage mainMenu;
    public void Play()
    {
        mainMenu.SelectGameMode(gameMode);
    }
    

}
