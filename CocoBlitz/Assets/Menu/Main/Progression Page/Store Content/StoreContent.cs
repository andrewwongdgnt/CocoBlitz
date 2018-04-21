using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreContent : MonoBehaviour {
    public MenuAudioManager menuAudioManager;
    public CardDisplay cardDisplay;
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        cardDisplay.SetActive(false);
    }

    public void DisplayCard(Card cardToDisplay)
    {
        menuAudioManager.PlayMainButtonClick();
        cardDisplay.SetActive(true);
        cardDisplay.Display(cardToDisplay);
    }

    public void HideCard()
    {
        menuAudioManager.PlayMainButtonClick();
        cardDisplay.SetActive(false);
    }
}
