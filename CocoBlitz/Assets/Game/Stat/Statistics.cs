using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Statistics {

    private string name;

    private List<StatisticsPoint> statisticsList =  new List<StatisticsPoint>();

    private float startTime = 0 ;
    private Card card;
    private Dictionary<CardManager.EntityEnum, CardManager.ColorEnum> entityToColor;
    private bool useCorrectColor;

    public float AverageTimeElapsed { get; private set; }
    public float AverageTimeElapsedForCorrectOnes { get; private set; }
    public float AverageTimeElapsedForCorrectOnesWithCorrectlyColored { get; private set; }
    public float AverageTimeElapsedForCorrectOnesWithIncorrectlyColored { get; private set; }
    public float AverageTimeElapsedForIncorrectOnes { get; private set; }
    public float AverageTimeElapsedForIncorrectOnesWithCorrectlyColored { get; private set; }
    public float AverageTimeElapsedForIncorrectOnesWithIncorrectlyColored { get; private set; }
    public int Total { get; private set; }
    public int TotalCorrect { get; private set; }
    public int TotalCorrectWithCorrectlyColored { get; private set; }
    public int TotalCorrectWithIncorrectlyColored { get; private set; }
    public int TotalIncorrect { get; private set; }
    public int TotalIncorrectWithCorrectlyColored { get; private set; }
    public int TotalIncorrectWithIncorrectlyColored { get; private set; }
    public int TotalMissed { get; private set; }
    public int TotalMissedWithCorrectlyColored { get; private set; }
    public int TotalMissedWithIncorrectlyColored { get; private set; }

    public Statistics(string name)
    {
        this.name = name;
    }

    public void Restart()
    {
        statisticsList = new List<StatisticsPoint>();
        startTime = 0;

        AverageTimeElapsed = 0;
        AverageTimeElapsedForCorrectOnes = 0;
        AverageTimeElapsedForCorrectOnesWithCorrectlyColored = 0;
        AverageTimeElapsedForCorrectOnesWithIncorrectlyColored = 0;
        AverageTimeElapsedForIncorrectOnes = 0;
        AverageTimeElapsedForIncorrectOnesWithCorrectlyColored = 0;
        AverageTimeElapsedForIncorrectOnesWithIncorrectlyColored = 0;
        Total = 0;
        TotalCorrect = 0;
        TotalCorrectWithCorrectlyColored = 0;
        TotalCorrectWithIncorrectlyColored = 0;
        TotalIncorrect = 0;
        TotalIncorrectWithCorrectlyColored = 0;
        TotalIncorrectWithIncorrectlyColored = 0;
        TotalMissed = 0;
        TotalMissedWithCorrectlyColored = 0;
        TotalMissedWithIncorrectlyColored = 0;
    }

    //Must be called before AddGuess and AddMissed
    public void AddPickedCard(float startTime, Card card, Dictionary<CardManager.EntityEnum, CardManager.ColorEnum> entityToColor, bool useCorrectColor)
    {
        this.startTime = startTime;
        this.card = card;
        this.entityToColor = entityToColor;
        this.useCorrectColor = useCorrectColor;
    }
    //Must be called after AddPickedCard
    public void AddMissed(CardManager.EntityEnum correctEntity)
    {
        statisticsList.Add(new StatisticsPoint(correctEntity, card, entityToColor, useCorrectColor));

        TotalMissed++;
        if (useCorrectColor)
        {
            TotalMissedWithCorrectlyColored++;
        }
        else
        {
            TotalMissedWithIncorrectlyColored++;
        }
    }

    //Must be called after AddPickedCard
    public void AddGuess(float endTime, CardManager.EntityEnum correctEntity, CardManager.EntityEnum guessedEntity)
    {
        float timeElapsed = endTime - startTime;
        statisticsList.Add(new StatisticsPoint(timeElapsed, correctEntity, guessedEntity, card, entityToColor, useCorrectColor));

        Total++;
        AverageTimeElapsed = (AverageTimeElapsed * (Total - 1) + timeElapsed) / Total;
        if (correctEntity == guessedEntity)
        {
            TotalCorrect++;
            AverageTimeElapsedForCorrectOnes = (AverageTimeElapsedForCorrectOnes * (TotalCorrect - 1) + timeElapsed) / TotalCorrect;
            if (useCorrectColor)
            {
                TotalCorrectWithCorrectlyColored++;
                AverageTimeElapsedForCorrectOnesWithCorrectlyColored = (AverageTimeElapsedForCorrectOnesWithCorrectlyColored * (TotalCorrectWithCorrectlyColored - 1) + timeElapsed) / TotalCorrectWithCorrectlyColored;
            }
            else
            {
                TotalCorrectWithIncorrectlyColored++;
                AverageTimeElapsedForCorrectOnesWithIncorrectlyColored = (AverageTimeElapsedForCorrectOnesWithIncorrectlyColored * (TotalCorrectWithIncorrectlyColored - 1) + timeElapsed) / TotalCorrectWithIncorrectlyColored;
            }
        }
        else
        { 
            TotalIncorrect++;
            AverageTimeElapsedForIncorrectOnes = (AverageTimeElapsedForIncorrectOnes * (TotalIncorrect - 1) + timeElapsed) / TotalIncorrect;
            if (useCorrectColor)
            {
                TotalIncorrectWithCorrectlyColored++;
                AverageTimeElapsedForIncorrectOnesWithCorrectlyColored = (AverageTimeElapsedForIncorrectOnesWithCorrectlyColored * (TotalIncorrectWithCorrectlyColored - 1) + timeElapsed) / TotalIncorrectWithCorrectlyColored;
            }
            else
            {
                TotalIncorrectWithIncorrectlyColored++;
                AverageTimeElapsedForIncorrectOnesWithIncorrectlyColored = (AverageTimeElapsedForIncorrectOnesWithIncorrectlyColored * (TotalIncorrectWithIncorrectlyColored - 1) + timeElapsed) / TotalIncorrectWithIncorrectlyColored;
            }
        }
    }

    public string GetStatsForPrint()
    {
        StringBuilder builder = new StringBuilder();

        //General stats
        builder.Append("================="+name+"===============\n");
        builder.Append("Average Elapsed Time: ").Append(AverageTimeElapsed).Append("\n");
        builder.Append(" - For Correct Ones: ").Append(AverageTimeElapsedForCorrectOnes).Append("\n");
        builder.Append("  -  For Correctly Colored Ones: ").Append(AverageTimeElapsedForCorrectOnesWithCorrectlyColored).Append("\n");
        builder.Append("  -  For Incorrectly Colored Ones: ").Append(AverageTimeElapsedForCorrectOnesWithIncorrectlyColored).Append("\n");
        builder.Append(" - For Incorrect Ones: ").Append(AverageTimeElapsedForIncorrectOnes).Append("\n");
        builder.Append("  -  For Correctly Colored Ones: ").Append(AverageTimeElapsedForIncorrectOnesWithCorrectlyColored).Append("\n");
        builder.Append("  -  For Incorrectly Colored Ones: ").Append(AverageTimeElapsedForIncorrectOnesWithIncorrectlyColored).Append("\n");
        builder.Append("Total: ").Append(Total).Append("\n");
        builder.Append(" - For Correct Ones: ").Append(TotalCorrect).Append("\n");
        builder.Append("  -  For Correctly Colored Ones: ").Append(TotalCorrectWithCorrectlyColored).Append("\n");
        builder.Append("  -  For Incorrectly Colored Ones: ").Append(TotalCorrectWithIncorrectlyColored).Append("\n");
        builder.Append(" - For Incorrect Ones: ").Append(TotalIncorrect).Append("\n");
        builder.Append("  -  For Correctly Colored Ones: ").Append(TotalIncorrectWithCorrectlyColored).Append("\n");
        builder.Append("  -  For Incorrectly Colored Ones: ").Append(TotalIncorrectWithIncorrectlyColored).Append("\n");
        builder.Append("Total Missed: ").Append(TotalMissed).Append("\n");
        builder.Append(" - For Correctly Colored Ones: ").Append(TotalMissedWithCorrectlyColored).Append("\n");
        builder.Append(" - For Incorrectly Colored Ones: ").Append(TotalMissedWithIncorrectlyColored).Append("\n");
        builder.Append("=============================================\n");

        //stats per guess
        statisticsList.ForEach(stat =>
        {

            builder.Append("=============================================\n");
            if (!stat.Missed)
            {
                builder.Append("Time Elapsed: ").Append(stat.TimeElapsed).Append("\n");
            }
            else
            {
                builder.Append("Missed\n");
            }
            builder.Append("Card ").Append(stat.UseCorrectColor ? "(Correctly colored)" : "(Incorrectly Colored)").Append("\n");

            Array.ForEach(stat.Card.cardEntities, cardEntity =>
            {

                CardManager.ColorEnum color = stat.EntityToColor[cardEntity.entity];
                builder.Append(" - ").Append(color).Append(" ").Append(cardEntity.entity).Append("\n");
            });

            if (!stat.Missed)
            {
                builder.Append("Correct Guess?: ").Append(stat.CorrectEntity == stat.GuessedEntity).Append("\n");
                builder.Append(" - Correct Entity: ").Append(stat.CorrectEntity).Append("\n");
                builder.Append(" - Guessed Entity: ").Append(stat.GuessedEntity).Append("\n");
            }
            else
            {
                builder.Append("Correct Entity: ").Append(stat.CorrectEntity).Append("\n");
            }
            builder.Append("=============================================\n");
        });

        return builder.ToString();
    }

}
