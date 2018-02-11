using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArea : MonoBehaviour {

    public enum NavigationEnum { Main, Settings, Tutorial, Credits, Help };

    public MainPage mainPage;
    public SettingsPage settingsPage;
    public CreditsPage creditsPage;
    public HelpPage helpPage;

    // Use this for initialization
    void Start () {
        ActivatePage(mainPage);
        if (GameUtil.fromGame)
        {
            GameUtil.fromGame = false;
            mainPage.SelectGameMode(GameUtil.currentGameMode);
        }
    }


    void ActivatePage(Page page)
    {

        mainPage.SetActive(false);
        settingsPage.SetActive(false);
        creditsPage.SetActive(false);
        helpPage.SetActive(false);

        page.SetActive(true);
    }

    public void NavigateTo(NavigationEnum nav)
    {
        if (nav == NavigationEnum.Main)
            ActivatePage(mainPage);
        else if (nav == NavigationEnum.Settings)
            ActivatePage(settingsPage);
        else if (nav == NavigationEnum.Credits)
            ActivatePage(creditsPage);
        else if (nav == NavigationEnum.Help)
            ActivatePage(helpPage);

    }
}
