using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour {

    public Text description;
    public Button buyButton;

    public Image thisImage;

    public Text buyBtnText;

    public ProgressStorePage progressStorePage;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void Enable(bool value)
    {

        ChangeAlpha(description, value ? 1 : 0.5f);

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
