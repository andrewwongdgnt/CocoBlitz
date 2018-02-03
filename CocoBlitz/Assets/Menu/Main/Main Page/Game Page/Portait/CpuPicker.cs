using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CpuPicker : MonoBehaviour {


    public Image portrait;
    public Text descriptionText;
    public Text nameText;
    public Button acceptBtn;

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
        bool available = cpu != null ? GameProgressionUtil.GetCpuAvailability(cpu) : false;
        portrait.sprite = cpu != null ? cpu.sprite : null;
        Color tempColor = portrait.color;
        tempColor.a = portrait.sprite == null ? 0 : 1;
        tempColor.r = available ? 255 : 0;
        tempColor.g = available ? 255 : 0;
        tempColor.b = available ? 255 : 0;
        portrait.color = tempColor;
        descriptionText.text = cpu != null ? (available ? cpu.description :cpu.unlockDescription) : "";
        nameText.text = cpu != null ? (available ? cpu.name : "???") : Cpu.NO_CPU;
        bool interactable = cpu == null || available;
        acceptBtn.interactable = interactable;
        Color tempButtonColor = acceptBtn.image.color;
        tempButtonColor.a = interactable ? 1 : 0.5f;
        acceptBtn.image.color = tempButtonColor;

        Text acceptBtnText = acceptBtn.GetComponentInChildren<Text>();
        Color tempButtonTextColor = acceptBtnText.color;
        tempButtonTextColor.a = interactable ? 1 : 0.5f;
        acceptBtnText.color = tempButtonTextColor;
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
        if (gamePage != null)
        {
            if (!cancel)
            {
                gamePage.UpdateCpuPortrait(cpuPortraitIndex, cpuPickedIndex);
            }
            gamePage.ShowNavArea(true);
        }
        gameObject.SetActive(false);
    }


}
