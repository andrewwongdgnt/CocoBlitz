using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpu : Participant
{

    //TODO-AW Adjust these stats later
    public const string KELSEY_NAME = "Kelsey";
    public static readonly Cpu KELSEY = new Cpu(KELSEY_NAME, "Kelsey desc", 1.9f, 2.9f, 90, 85);
    public const string ANDREW_NAME = "Andrew";
    public static readonly Cpu ANDREW = new Cpu(ANDREW_NAME, "Andrew desc", 1.5f, 2.1F, 90, 85);
    public const string MONKEY_NAME = "Monkey";
    public static readonly Cpu MONKEY = new Cpu(MONKEY_NAME, "Monkey desc", 1.5f, 2.1F, 90, 85);
    public const string PENGUIN_NAME = "Penguin";
    public static readonly Cpu PENGUIN = new Cpu(PENGUIN_NAME, "Penguin desc", 1.5f, 2.1F, 90, 85);
    public const string KONGO_NAME = "Kongo";
    public static readonly Cpu KONGO = new Cpu(KONGO_NAME, "Kongo desc", 1.5f, 2.1F, 90, 85);
    public const string PURPLE_MONKEY_NAME = "Purple Monkey";
    public static readonly Cpu PURPLE_MONKEY = new Cpu(PURPLE_MONKEY_NAME, "Purple Monkey desc", 1.5f, 2.1F, 90, 85);
    public const string MUFFIN_NAME = "Muffin";
    public static readonly Cpu MUFFIN = new Cpu(MUFFIN_NAME, "Muffin desc", .8f, 1.2F, 95, 90);
    public const string CHOMP_NAME = "Chomp";
    public static readonly Cpu CHOMP = new Cpu(CHOMP_NAME, "Chomp desc", 1.5f, 2.0F, 85, 80, 
        new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Chomp,-0.5f } },
        new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Chomp, 5 } });
    public const string COCO_NAME = "Coco";
    public static readonly Cpu COCO = new Cpu(COCO_NAME, "Coco desc", 1.5f, 2.0F, 85, 80,
        new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Coco, -0.5f } },
        new Dictionary<CardUtil.EntityEnum, float> { { CardUtil.EntityEnum.Coco, 5 } });

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
        , Dictionary<CardUtil.EntityEnum, float> delayModifer = null, Dictionary<CardUtil.EntityEnum, float> chanceOfCorrectModifer = null) : base(name)
    {
        this.description = description;
        this.delayLowerRangeBeforeAnswer = delayLowerRangeBeforeAnswer;
        this.delayUpperRangeBeforeAnswer = delayUpperRangeBeforeAnswer;
        this.chanceOfCorrectForCorrectlyColored = chanceOfCorrectForCorrectlyColored;
        this.chanceOfCorrectForIncorrectlyColored = chanceOfCorrectForIncorrectlyColored;
        delayModiferDict = delayModifer;
        chanceOfCorrectModiferDict = chanceOfCorrectModifer;

    }



}
