using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpu : Participant
{

    public static readonly string NO_CPU = "No CPU";

    //TODO-AW Adjust these stats later
    public static readonly Cpu KELSEY = new Builder()
        .Name("Kelsey")
        .Sprite(Resources.Load<Sprite>("Portraits/Kelsey Portrait"))
        .Description("Kelsey just likes playing games.  She's still going to try, but her main goal is just to have fun.")
        .DelayLowerRangeBeforeAnswer(1.9f)
        .DelayUpperRangeBeforeAnswer(2.9f)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .IsStarter()
        .Build();
    public static readonly Cpu ANDREW = new Builder()
        .Name("Andrew")
        .Sprite(Resources.Load<Sprite>("Portraits/Andrew Portrait"))
        .Description("Andrew is very competitive and so he can be a tough opponent to beat.  Are you up to the challenge?")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .IsStarter()
        .Build();
    public static readonly Cpu MONKEY = new Builder()
        .Name("Monkey")
        .Sprite(Resources.Load<Sprite>("Portraits/Monkey Portrait"))
        .Description("Don't let the Monkey's profile picture fool you.  He may look playful, but when it comes to CoCo Go then he's not monkeying around.")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .IsStarter()
        .Build();
    public static readonly Cpu PENGUIN = new Builder()
        .Name("Penguin")
        .Sprite(Resources.Load<Sprite>("Portraits/Penguin Portrait"))
        .Description("Penguin may be small, but what she lacks in size then she more than makes up for with enthusiasm.  Go Penguin!")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .IsStarter()
        .Build();
    public static readonly Cpu KONGO = new Builder()
        .Name("Kongo")
        .Sprite(Resources.Load<Sprite>("Portraits/Kongo Portrait"))
        .Description("Kongo is super smart so he almost never gets it wrong. So what if it takes a bit longer to figure it out? If you're going to do something then you might as well do it right.")
        .UnlockDescription("Unlock this character by...?")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu PURPLE_MONKEY = new Builder()
        .Name("Purple Monkey")
        .Sprite(Resources.Load<Sprite>("Portraits/Purple Monkey Portrait"))
        .Description("Who cares if you get one wrong here and there?  Life's too short to always wait for the sure thing so when the Purple Monkey sees an opportunity then you'd better believe she's going to take it.")
        .UnlockDescription("Unlock this character by...?")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.1F)
        .ChanceOfCorrectForCorrectlyColored(90)
        .ChanceOfCorrectForIncorrectlyColored(85)
        .Build();
    public static readonly Cpu MUFFIN = new Builder()
        .Name("Muffin")
        .Sprite(Resources.Load<Sprite>("Portraits/Muffin Portrait"))
        .Description("Muffin is Coco's best friend and they love to hang out together. But lately, Coco seems to always wander off and get lost. Luckily, Muffin is very good at finding Coco.")
        .UnlockDescription("Unlock this character by...?")
        .DelayLowerRangeBeforeAnswer(.8f)
        .DelayUpperRangeBeforeAnswer(1.2F)
        .ChanceOfCorrectForCorrectlyColored(95)
        .ChanceOfCorrectForIncorrectlyColored(90)
        .Build();
    public static readonly Cpu CHOMP = new Builder()
        .Name("Chomp")
        .Sprite(Resources.Load<Sprite>("Portraits/Chomp Portrait"))
        .Description("Chomp desc")
        .UnlockDescription("Unlock this character by...?")
        .DelayLowerRangeBeforeAnswer(1.5f)
        .DelayUpperRangeBeforeAnswer(2.0F)
        .ChanceOfCorrectForCorrectlyColored(85)
        .ChanceOfCorrectForIncorrectlyColored(80) 
        .DelayModiferDict(new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Chomp,-0.5f } })
        .ChanceOfCorrectModiferDict(new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Chomp, 5 } })
        .Build();
    public static readonly Cpu COCO = new Builder()
        .Name("Coco")
        .Sprite(Resources.Load<Sprite>("Portraits/Coco Portrait"))
        .Description("Coco's got the brains, the brawn and even the whole game named after him. You didn't think he'd just sit around and watch everyone else play, did you?")
        .UnlockDescription("Unlock this character by...?")
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
    public string unlockDescription { get; private set; }

    public bool starter { get; private set; }

    public Cpu(string name, string description, string unlockDescription, float delayLowerRangeBeforeAnswer,float  delayUpperRangeBeforeAnswer,  float chanceOfCorrectForCorrectlyColored, float chanceOfCorrectForIncorrectlyColored
        , Dictionary<CardUtil.EntityEnum, float> delayModiferDict, Dictionary<CardUtil.EntityEnum, float> chanceOfCorrectModiferDict, bool starter) : base(name)
    {
        this.description = description;
        this.unlockDescription = unlockDescription;
        this.delayLowerRangeBeforeAnswer = delayLowerRangeBeforeAnswer;
        this.delayUpperRangeBeforeAnswer = delayUpperRangeBeforeAnswer;
        this.chanceOfCorrectForCorrectlyColored = chanceOfCorrectForCorrectlyColored;
        this.chanceOfCorrectForIncorrectlyColored = chanceOfCorrectForIncorrectlyColored;
        this.delayModiferDict = delayModiferDict;
        this.chanceOfCorrectModiferDict = chanceOfCorrectModiferDict;
        this.starter = starter;
    }

    public override Participant RebuildToPlay()
    {
        return new Builder()
        .Name(name)
        .Sprite(sprite)
        .DelayLowerRangeBeforeAnswer(delayLowerRangeBeforeAnswer)
        .DelayUpperRangeBeforeAnswer(delayUpperRangeBeforeAnswer)
        .ChanceOfCorrectForCorrectlyColored(chanceOfCorrectForCorrectlyColored)
        .ChanceOfCorrectForIncorrectlyColored(chanceOfCorrectForIncorrectlyColored)
        .DelayModiferDict(delayModiferDict)
        .ChanceOfCorrectModiferDict(chanceOfCorrectModiferDict)
        .Build();
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

        private string unlockDescription;
        public Builder UnlockDescription(string unlockDescription)
        {
            this.unlockDescription = unlockDescription;
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

        private bool starter;
        public Builder IsStarter()
        {
            starter = true;
            return this;
        }

        private Sprite sprite;
        public Builder Sprite(Sprite sprite)
        {
            this.sprite = sprite;
            return this;
        }

        public Cpu Build()
        {
            Cpu cpu = new Cpu(name,
                description,
                unlockDescription,
                delayLowerRangeBeforeAnswer,
                delayUpperRangeBeforeAnswer,
                chanceOfCorrectForCorrectlyColored,
                chanceOfCorrectForIncorrectlyColored,
                delayModiferDict,
                chanceOfCorrectModiferDict,
                starter);
            cpu.sprite = sprite;
            return cpu;
        }
    }

    public override bool Equals(object obj)
    {
        Cpu cpu = obj as Cpu;

        return cpu.name == name;
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }

}
