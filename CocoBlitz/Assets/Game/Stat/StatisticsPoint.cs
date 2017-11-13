using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsPoint {

    public float TimeElapsed { get; private set; }
    public CardManager.EntityEnum CorrectEntity { get; private set; }
    public CardManager.EntityEnum GuessedEntity { get; private set; }
    public Card Card { get; private set; }
    public Dictionary<CardManager.EntityEnum, CardManager.ColorEnum> EntityToColor { get; private set; }
    public bool UseCorrectColor { get; private set; }

    public bool Missed;

    public StatisticsPoint(float timeElapsed, CardManager.EntityEnum correctEntity, CardManager.EntityEnum guessedEntity, Card card, Dictionary<CardManager.EntityEnum, CardManager.ColorEnum> entityToColor, bool useCorrectColor)
    {
        TimeElapsed = timeElapsed;
        CorrectEntity = correctEntity;
        GuessedEntity = guessedEntity;
        Card = card;
        EntityToColor = entityToColor;
        UseCorrectColor = useCorrectColor;
        Missed = false;
    }

    public StatisticsPoint(CardManager.EntityEnum correctEntity, Card card, Dictionary<CardManager.EntityEnum, CardManager.ColorEnum> entityToColor, bool useCorrectColor)
    {
        CorrectEntity = correctEntity;
        Card = card;
        EntityToColor = entityToColor;
        UseCorrectColor = useCorrectColor;
        Missed = true;
    }

}
