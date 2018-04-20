﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreCardItem : StoreItem
{
    public Image cardIcon;
    public Image cardDisplay;


    public GameProgressionUtil.BuyableCardEnum buyableCardEnum;

    // Use this for initialization
    void Start()
    {
        
        //Check if card already bought
        EnableCard(!GameProgressionUtil.GetCardAvailability(buyableCardEnum));

        UpdatePrice(GameProgressionUtil.CARD_COST_MAP[buyableCardEnum]);

    }

    public void Buy()
    {
        menuAudioManager.PlayBuyButtonClick();
        GameProgressionUtil.BuyStatus buyStatus = GameProgressionUtil.BuyCard(buyableCardEnum);
        if (buyStatus == GameProgressionUtil.BuyStatus.SUCCESSFUL)
        {
            EnableCard(false);
            progressStorePage.UpdateBananaCount();
        }
    }

    private void EnableCard(bool value)
    {
        Enable(value);

        ChangeAlpha(description, value ? 1 : 0);
        ChangeAlpha(cardIcon, value ? 1 : 0);
        ChangeAlpha(cardDisplay, value ? 0 : 1);
        Array.ForEach(cardDisplay.GetComponentsInChildren<Image>(), im =>
        {
            ChangeAlpha(im, value ? 0 : 1);            
        });
    }
}
