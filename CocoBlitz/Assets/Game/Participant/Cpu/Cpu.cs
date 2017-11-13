using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpu : Participant
{

    public static Cpu KELSEY = new Cpu("Kelsey",1.9f, 2.9f, 90, 85);
    public static Cpu ANDREW = new Cpu("Andrew", 1.5f, 2.1F, 90, 85);
    public static Cpu MUFFIN = new Cpu("Muffin", .8f, 1.2F, 95, 90);

    public float DelayLowerRangeBeforeAnswer { get; set; }
    public float DelayUpperRangeBeforeAnswer { get; set; }

    //number between 0 and 100
    public float ChanceOfCorrectForCorrectlyColored { get; set; }
    public float ChanceOfCorrectForIncorrectlyColored { get; set; }


    public Cpu(string name, float delayLowerRangeBeforeAnswer,float  delayUpperRangeBeforeAnswer,  float chanceOfCorrectForCorrectlyColored, float chanceOfCorrectForIncorrectlyColored) : base(name)
    {

        DelayLowerRangeBeforeAnswer = delayLowerRangeBeforeAnswer;
        DelayUpperRangeBeforeAnswer = delayUpperRangeBeforeAnswer;
        ChanceOfCorrectForCorrectlyColored = chanceOfCorrectForCorrectlyColored;
        ChanceOfCorrectForIncorrectlyColored = chanceOfCorrectForIncorrectlyColored;

    }



}
