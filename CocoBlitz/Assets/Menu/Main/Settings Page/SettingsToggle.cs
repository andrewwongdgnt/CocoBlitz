using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsToggle : MonoBehaviour {
    public MenuAudioManager menuAudioManager;
    public SettingsPage.SettingsEnum settings;
    public SettingsPage settingsPage;
    public void SetSettings(bool value)
    {
        menuAudioManager.PlayToggleClick();
        settingsPage.SetSettings(settings, value);
    }


}
