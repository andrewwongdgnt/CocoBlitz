using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButton : MonoBehaviour {


    

    public NavigationArea.NavigationEnum nav;
    public NavigationArea navArea;
    public MenuAudioManager menuAudioManager;
    public void NavigateTo()
    {
        menuAudioManager.PlayNavButtonClick();
        navArea.NavigateTo(nav);
    }
}
