using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public string labelString;
    public Text label;
    public Text requirement;
    public Slider slider;
    private void Start()
    {
        label.text = labelString;
        slider.enabled = false;
    }

    public void SetValue(int current, int max)
    {
        requirement.text = current + "/" + max;
        slider.value = (float) current/max;
    }
}
