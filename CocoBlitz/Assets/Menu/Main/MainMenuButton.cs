using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour {

    public GameManager.GameModeEnum gameMode;
    public MainMenu mainMenu;
    public void Play()
    {
        mainMenu.PlayGame(gameMode);
    }

}
