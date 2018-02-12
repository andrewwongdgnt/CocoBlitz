using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CpuPortrait : MonoBehaviour
{
    public Button button;
    public Image portrait;
    public Text nameText;

    public void SetCpuDisplay(string name, Sprite sprite)
    {
        nameText.text = name;
        portrait.sprite = sprite;
        Color tempColor = portrait.color;
        tempColor.a = sprite == null ? 0 : 1;
        portrait.color = tempColor;
        button.interactable = true;
    }
    
}
