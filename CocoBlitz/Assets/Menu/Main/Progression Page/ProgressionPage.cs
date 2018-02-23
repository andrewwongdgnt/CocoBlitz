using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionPage : MonoBehaviour, Page
{
    public enum ProgressionPageEnum { CocoStore, BananaProgress };
    public ProgressStorePage progressStorePage;
    public GameObject progressionGroup;

    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
        DeactivateAllGroup();
        progressionGroup.SetActive(true);
    }


    void DeactivateAllGroup()
    {

        progressionGroup.SetActive(false);
        progressStorePage.SetActive(false);

    }

    public void SelectProgressionPage(ProgressionPageEnum progressionPageEnum)
    {

        DeactivateAllGroup();
        progressStorePage.SetActive(true);
        progressStorePage.SetPageType(progressionPageEnum);
    }
    
}
