using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    public Text requirement;
    public Text nextReward;
    public Slider slider;
    private void Start()
    {
        slider.enabled = false;
    }

    public void SetValue(float current, float max)
    {
        requirement.text = current + "/" + max;
        slider.value =  current/max;
    }
    public void SetNextReward(float value)
    {

        nextReward.text = "x" + value;
    }
}
