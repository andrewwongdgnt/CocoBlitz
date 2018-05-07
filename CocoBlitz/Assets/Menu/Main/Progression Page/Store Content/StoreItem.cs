using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StoreItem : MonoBehaviour {
    public MenuAudioManager menuAudioManager;
    public Text description;
    public Button buyButton;

    public Image thisImage;

    public Text buyBtnText;

    public ProgressStorePage progressStorePage;

    public void Buy()
    {
        GameProgressionUtil.BuyStatus buyStatus = BuyItem();
        if (buyStatus == GameProgressionUtil.BuyStatus.SUCCESSFUL)
        {
            menuAudioManager.PlayBuyButtonClick();
            EnableItem(false);
            progressStorePage.UpdateBananaCount();
        }
        else if (buyStatus == GameProgressionUtil.BuyStatus.NOT_ENOUGH_BANANAS)
        {
            //TODO-AW play not enough bananas audio
        }
    }

    protected abstract GameProgressionUtil.BuyStatus BuyItem();

    protected abstract void EnableItem(bool value);

    protected void Enable(bool value)
    {

        ChangeAlpha(buyButton.image, value ? 1 : 0.5f);
        buyButton.interactable = value;
        
        ChangeAlpha(buyBtnText, value ? 1 : 0.5f);
        Image bananaIcon = buyBtnText.GetComponentInChildren<Image>();
        ChangeAlpha(bananaIcon, value ? 1 : 0.5f);

        ChangeAlpha(thisImage, value ? 1 : 0.5f);

    }


    protected static void ChangeAlpha(Graphic g, float a)
    {

        Color c = g.color;
        c.a = a;
        g.color = c;
    }

    protected void UpdatePrice(int price)
    {
        buyBtnText.text = "x"+price.ToString();
    }
}
