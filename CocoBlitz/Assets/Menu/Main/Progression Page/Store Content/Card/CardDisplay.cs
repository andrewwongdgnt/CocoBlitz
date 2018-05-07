using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDisplay : MonoBehaviour {

    public GameObject cardBackground;

    private Card cardToDisplayGameObject;
    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void Display(Card cardToDisplay)
    {
        if (cardToDisplayGameObject != null)
        {
            DestroyImmediate(cardToDisplayGameObject.gameObject);
            cardToDisplayGameObject = null;
        }
        cardToDisplayGameObject = Instantiate(cardToDisplay, cardBackground.transform);
        Array.ForEach(cardToDisplayGameObject.cardEntities, cardEntity =>
        {
            CardUtil.ColorEnum color = CardUtil.EntityColorMap[cardEntity.entity];
            cardEntity.image.color = CardUtil.ColorColorMap[color];

        });

    }
}
