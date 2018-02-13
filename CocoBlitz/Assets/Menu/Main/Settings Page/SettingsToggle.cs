using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsToggle : MonoBehaviour {

    public SettingsPage.SettingsEnum settings;
    public SettingsPage settingsPage;
    public void SetSettings(bool value)
    {
        settingsPage.SetSettings(settings, value);
    }


}
