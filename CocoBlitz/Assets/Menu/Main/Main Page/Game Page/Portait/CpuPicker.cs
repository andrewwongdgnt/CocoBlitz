using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CpuPicker : MonoBehaviour {


    public Image portrait;
    public Text descriptionText;
    public Text nameText;

    private int cpuPortraitIndex;
    private int cpuPickedIndex;
    private List<Cpu> allCpus;

    private GamePage gamePage;


    public void Open(GamePage gamePage, int cpuPortraitIndex, int cpuPickedIndex, List<Cpu> allCpus)
    {
        gameObject.SetActive(true);
        this.gamePage = gamePage;
        this.cpuPortraitIndex = cpuPortraitIndex;
        this.cpuPickedIndex = cpuPickedIndex;
        this.allCpus = allCpus;

        SetCpuDisplay(allCpus[cpuPickedIndex]);
    }

    private void SetCpuDisplay(Cpu cpu)
    {

        portrait.sprite = cpu != null ? cpu.sprite : null;
        Color tempColor = portrait.color;
        tempColor.a = portrait.sprite == null ? 0 : 1;
        portrait.color = tempColor;
        descriptionText.text = cpu != null ? cpu.description : "";
        nameText.text = cpu != null ? cpu.name : "None";
    }

    public void NextCpu()
    {
        if (cpuPickedIndex>= allCpus.Count - 1)
        {
            cpuPickedIndex = 0;
        }
        else
        {
            cpuPickedIndex++;
        }

        SetCpuDisplay(allCpus[cpuPickedIndex]);
    }

    public void PrevCpu()
    {

        if (cpuPickedIndex <=0)
        {
            cpuPickedIndex = allCpus.Count - 1;
        }
        else
        {
            cpuPickedIndex--;
        }

        SetCpuDisplay(allCpus[cpuPickedIndex]);
    }

    public void Close(bool cancel)
    {
        if (!cancel)
        {
            gamePage.UpdateCpuPortrait(cpuPortraitIndex, cpuPickedIndex);
        }
        gameObject.SetActive(false);
    }


}
