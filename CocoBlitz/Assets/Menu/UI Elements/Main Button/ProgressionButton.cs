using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionButton : MonoBehaviour {

    public ProgressionPage.ProgressionPageEnum progressPageEnum;
    public ProgressionPage progressionPage;
    public void SelectBananaProgressPage()
    {
        progressionPage.SelectProgressionPage(progressPageEnum);
    }
}
