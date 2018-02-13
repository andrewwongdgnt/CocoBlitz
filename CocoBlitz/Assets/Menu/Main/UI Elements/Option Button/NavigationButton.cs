using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationButton : MonoBehaviour {


    

    public NavigationArea.NavigationEnum nav;
    public NavigationArea navArea;
    public void NavigateTo()
    {
        navArea.NavigateTo(nav);
    }
}
