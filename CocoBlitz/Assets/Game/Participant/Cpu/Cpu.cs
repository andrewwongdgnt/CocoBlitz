using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpu : Participant
{

    public static readonly string NO_CPU = "No CPU";

    //TODO-AW Adjust these stats later
    public static readonly Cpu KELSEY = new Builder()
        .Name("Kelsey")
        .Description("Kelsey desc")
        .DelayLowerRangeBeforeAnswer(1.9f)
        .DelayUpperRangeBeforeAnswer(2.9f)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu ANDREW = new Builder()
        .Name("Andrew")
        .Description("Andrew desc")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu MONKEY = new Builder()
        .Name("Monkey")
        .Description("Monkey desc")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu PENGUIN = new Builder()
        .Name("Penguin")
        .Description("Penguin desc")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu KONGO = new Builder()
        .Name("Kongo")
        .Description("Kongo is super smart so he almost never gets it wrong. So what if it takes a bit longer to figure it out? If you're going to do something then you might as well do it right.")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu PURPLE_MONKEY = new Builder()
        .Name("Purple Monkey")
        .Description("Purple Monkey desc")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu MUFFIN = new Builder()
        .Name("Muffin")
        .Description("Muffin desc")
        .DelayLowerRangeBeforeAnswer(.8f)
        .DelayUpperRangeBeforeAnswer(1.2F)
        .ChanceOfCorrectForCorrectlyColored(95)
        .ChanceOfCorrectForIncorrectlyColored(90)
        .Build();
    public static readonly Cpu CHOMP = new Builder()
        .Name("Chomp")
        .Description("Chomp desc")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.0F)
        .ChanceOfCorrectForCorrectlyColored(85)
        .ChanceOfCorrectForIncorrectlyColored(80) 
        .DelayModiferDict(new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Chomp,-0.5f } })
        .ChanceOfCorrectModiferDict(new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Chomp, 5 } })
        .Build();
    public static readonly Cpu COCO = new Builder()
        .Name("Coco")
        .Description("Coco desc")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.0F)
        .ChanceOfCorrectForCorrectlyColored(85)
        .ChanceOfCorrectForIncorrectlyColored(80)
        .DelayModiferDict(new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Coco, -0.5f } })
        .ChanceOfCorrectModiferDict(new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Coco, 5 } })
        .Build();

    public float delayLowerRangeBeforeAnswer { get; set; }
    public float delayUpperRangeBeforeAnswer { get; set; }

    //number between 0 and 100
    public float chanceOfCorrectForCorrectlyColored { get; set; }
    public float chanceOfCorrectForIncorrectlyColored { get; set; }

    public Dictionary<CardUtil.EntityEnum, float> delayModiferDict { get; set; }
    public Dictionary<CardUtil.EntityEnum, float> chanceOfCorrectModiferDict { get; set; }

    public Sprite sprite { get; set; }

    public string description { get; private set; }

    public Cpu(string name, string description, float delayLowerRangeBeforeAnswer,float  delayUpperRangeBeforeAnswer,  float chanceOfCorrectForCorrectlyColored, float chanceOfCorrectForIncorrectlyColored
        , Dictionary<CardUtil.EntityEnum, float> delayModiferDict, Dictionary<CardUtil.EntityEnum, float> chanceOfCorrectModiferDict) : base(name)
    {
        this.description = description;
        this.delayLowerRangeBeforeAnswer = delayLowerRangeBeforeAnswer;
        this.delayUpperRangeBeforeAnswer = delayUpperRangeBeforeAnswer;
        this.chanceOfCorrectForCorrectlyColored = chanceOfCorrectForCorrectlyColored;
        this.chanceOfCorrectForIncorrectlyColored = chanceOfCorrectForIncorrectlyColored;
        this.delayModiferDict = delayModiferDict;
        this.chanceOfCorrectModiferDict = chanceOfCorrectModiferDict;

    }

    public class Builder
    {
        private string name;
        public Builder Name(string name)
        {
            this.name = name;
            return this;
        }

        private string description;
        public Builder Description(string description)
        {
            this.description = description;
            return this;
        }

        private float delayLowerRangeBeforeAnswer;
        public Builder DelayLowerRangeBeforeAnswer(float delayLowerRangeBeforeAnswer)
        {
            this.delayLowerRangeBeforeAnswer = delayLowerRangeBeforeAnswer;
            return this;
        }

        private float delayUpperRangeBeforeAnswer;
        public Builder DelayUpperRangeBeforeAnswer(float delayUpperRangeBeforeAnswer)
        {
            this.delayUpperRangeBeforeAnswer = delayUpperRangeBeforeAnswer;
            return this;
        }

        private float chanceOfCorrectForCorrectlyColored;
        public Builder ChanceOfCorrectForCorrectlyColored(float chanceOfCorrectForCorrectlyColored)
        {
            this.chanceOfCorrectForCorrectlyColored = chanceOfCorrectForCorrectlyColored;
            return this;
        }

        private float chanceOfCorrectForIncorrectlyColored;
        public Builder ChanceOfCorrectForIncorrectlyColored(float chanceOfCorrectForIncorrectlyColored)
        {
            this.chanceOfCorrectForIncorrectlyColored = chanceOfCorrectForIncorrectlyColored;
            return this;
        }

        private Dictionary<CardUtil.EntityEnum, float> delayModiferDict;
        public Builder DelayModiferDict(Dictionary<CardUtil.EntityEnum, float> delayModiferDict)
        {
            this.delayModiferDict = delayModiferDict;
            return this;
        }

        private Dictionary<CardUtil.EntityEnum, float> chanceOfCorrectModiferDict;
        public Builder ChanceOfCorrectModiferDict(Dictionary<CardUtil.EntityEnum, float> chanceOfCorrectModiferDict)
        {
            this.chanceOfCorrectModiferDict = chanceOfCorrectModiferDict;
            return this;
        }

        public Cpu Build()
        {
            return new Cpu(name, 
                description, 
                delayLowerRangeBeforeAnswer, 
                delayUpperRangeBeforeAnswer,
                chanceOfCorrectForCorrectlyColored,
                chanceOfCorrectForIncorrectlyColored,
                delayModiferDict,
                chanceOfCorrectModiferDict);
        }
    }



}
