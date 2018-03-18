using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionButton : MonoBehaviour {

    public ProgressionPage.ProgressionPageEnum progressPageEnum;
    public ProgressionPage progressionPage;
    public MenuAudioManager menuAudioManager;
    public void SelectBananaProgressPage()
    {
        menuAudioManager.PlayMainButtonClick();
        progressionPage.SelectProgressionPage(progressPageEnum);
    }
}
