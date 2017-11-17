using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArea : MonoBehaviour {

    public enum NavigationEnum { Main, Settings, Tutorial, Credits };

    public MainPage mainPage;
    public SettingsPage settingsPage;

    // Use this for initialization
    void Start () {
        ActivatePage(mainPage);
    }


    void ActivatePage(Page page)
    {

        mainPage.SetActive(false);
        settingsPage.SetActive(false);

        page.SetActive(true);
    }

    public void NavigateTo(NavigationEnum nav)
    {
        if (nav == NavigationEnum.Main)
            ActivatePage(mainPage);
        else if (nav == NavigationEnum.Settings)
            ActivatePage(settingsPage);

    }
}
