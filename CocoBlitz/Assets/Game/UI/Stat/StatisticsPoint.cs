using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsPoint {

    public float TimeElapsed { get; private set; }
    public CardUtil.EntityEnum CorrectEntity { get; private set; }
    public CardUtil.EntityEnum GuessedEntity { get; private set; }
    public Card Card { get; private set; }
    public Dictionary<CardUtil.EntityEnum, CardUtil.ColorEnum> EntityToColor { get; private set; }
    public bool UseCorrectColor { get; private set; }

    public bool Missed;

    public StatisticsPoint(float timeElapsed, CardUtil.EntityEnum correctEntity, CardUtil.EntityEnum guessedEntity, Card card, Dictionary<CardUtil.EntityEnum, CardUtil.ColorEnum> entityToColor, bool useCorrectColor)
    {
        TimeElapsed = timeElapsed;
        CorrectEntity = correctEntity;
        GuessedEntity = guessedEntity;
        Card = card;
        EntityToColor = entityToColor;
        UseCorrectColor = useCorrectColor;
        Missed = false;
    }

    public StatisticsPoint(CardUtil.EntityEnum correctEntity, Card card, Dictionary<CardUtil.EntityEnum, CardUtil.ColorEnum> entityToColor, bool useCorrectColor)
    {
        CorrectEntity = correctEntity;
        Card = card;
        EntityToColor = entityToColor;
        UseCorrectColor = useCorrectColor;
        Missed = true;
    }

}
