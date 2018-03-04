using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCpuItem : StoreItem {


    public Image portraitFrame;
    public Image portrait;


    public GameProgressionUtil.BuyableCpuEnum buyableCpuEnum;

    // Use this for initialization
    void Start () {

        //Check if cpu already bought
        Cpu cpu = GameProgressionUtil.CPU_MAP[buyableCpuEnum];

        portrait.sprite = cpu.sprite;
        description.text = cpu.name;

        EnableCpu(!GameProgressionUtil.GetCpuAvailability(cpu));

        UpdatePrice(GameProgressionUtil.CPU_COST_MAP[buyableCpuEnum]);
    
    }
	
	public void Buy () {
        GameProgressionUtil.BuyStatus buyStatus = GameProgressionUtil.BuyCpu(buyableCpuEnum);
        if (buyStatus == GameProgressionUtil.BuyStatus.SUCCESSFUL)
        {
            EnableCpu(false);
            progressStorePage.UpdateBananaCount();
        }
    }

    private void EnableCpu(bool value)
    {
        Enable(value);

        ChangeAlpha(portraitFrame, value ? 1 : 0.5f);
        ChangeAlpha(portrait, value ? 1 : 0.5f);
    }
}
