﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPage : MonoBehaviour, Page {

    

    public GamePage gamePage;
    public GameObject mainGroup;

   

    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
        DeactivateAllGroup();
        mainGroup.SetActive(true);
    }

    void DeactivateAllGroup()
    {

        mainGroup.SetActive(false);
        gamePage.SetActive(false);
        
    }

    public void PlayOffline()
    {


        DeactivateAllGroup();
        gamePage.SetActive(true);

    }
    


}
