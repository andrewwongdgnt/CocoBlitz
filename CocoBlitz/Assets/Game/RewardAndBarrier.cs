

using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardAndBarrier
{
    
    public readonly static RewardAndBarrier[] TOTAL_SINGLE_PLAYER_GAMES_PLAYED_PROGRESSION
        = new RewardAndBarrier[] {
            new RewardAndBarrier(10,5)
            ,new RewardAndBarrier(30,10)
            ,new RewardAndBarrier(60,15)
            ,new RewardAndBarrier(100,20)
            ,new RewardAndBarrier(150,25)
            ,new RewardAndBarrier(210,30)
            ,new RewardAndBarrier(280,35)
            ,new RewardAndBarrier(360,40)
            ,new RewardAndBarrier(450,45)
            ,new RewardAndBarrier(550,50)
            ,new RewardAndBarrier(660,55)
            ,new RewardAndBarrier(780,60)
            ,new RewardAndBarrier(910,65)
            ,new RewardAndBarrier(1050,70)
            ,new RewardAndBarrier(1200,75)};

    public readonly static RewardAndBarrier[] TOTAL_TWO_PLAYER_GAMES_PLAYED_PROGRESSION
     = TOTAL_SINGLE_PLAYER_GAMES_PLAYED_PROGRESSION;

    public readonly static RewardAndBarrier[] TOTAL_TIME_SPENT_PLAYING_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(60,5) //1 min
            ,new RewardAndBarrier(300,10) //5 min
            ,new RewardAndBarrier(600,15) //10 min
            ,new RewardAndBarrier(900,20) // 15 min
            ,new RewardAndBarrier(1800,25) // 30 min
            ,new RewardAndBarrier(2700,30) // 45 min
            ,new RewardAndBarrier(3600,35) // 60 min
            ,new RewardAndBarrier(5400,40) // 90 min
            ,new RewardAndBarrier(7200,45)}; // 120 min


    public readonly static RewardAndBarrier[] TOTAL_CPUS_DEFEATED_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(20,5)
            ,new RewardAndBarrier(50,10)
            ,new RewardAndBarrier(90,15)
            ,new RewardAndBarrier(140,20)
            ,new RewardAndBarrier(200,25)
            ,new RewardAndBarrier(270,30)
            ,new RewardAndBarrier(350,35)
            ,new RewardAndBarrier(440,40)
            ,new RewardAndBarrier(540,45)
            ,new RewardAndBarrier(650,50)
            ,new RewardAndBarrier(770,55)
            ,new RewardAndBarrier(900,60)
            ,new RewardAndBarrier(1040,65)
            ,new RewardAndBarrier(1190,70)
            ,new RewardAndBarrier(1350,75)};

    public readonly static RewardAndBarrier[] TOTAL_CORRECT_CORRECTLY_COLORED_GUESSES_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(25,5)
            ,new RewardAndBarrier(50,10)
            ,new RewardAndBarrier(75,15)
            ,new RewardAndBarrier(100,20)
            ,new RewardAndBarrier(150,25)
            ,new RewardAndBarrier(200,30)
            ,new RewardAndBarrier(250,35)
            ,new RewardAndBarrier(300,40)
            ,new RewardAndBarrier(400,45)
            ,new RewardAndBarrier(500,50)
            ,new RewardAndBarrier(750,55)
            ,new RewardAndBarrier(1000,60)
            ,new RewardAndBarrier(1250,65)
            ,new RewardAndBarrier(1500,70)
            ,new RewardAndBarrier(2000,75)};

    public readonly static RewardAndBarrier[] TOTAL_CORRECT_INCORRECTLY_COLORED_GUESSES_PROGRESSION
     = TOTAL_CORRECT_CORRECTLY_COLORED_GUESSES_PROGRESSION;

    public readonly static RewardAndBarrier[] TOTAL_CORRECT_GUESSES_UNDER_ONE_SECOND_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(5,5)
            ,new RewardAndBarrier(10,10)
            ,new RewardAndBarrier(15,15)
            ,new RewardAndBarrier(20,20)
            ,new RewardAndBarrier(25,25)
            ,new RewardAndBarrier(30,30)
            ,new RewardAndBarrier(40,35)
            ,new RewardAndBarrier(50,40)
            ,new RewardAndBarrier(75,45)
            ,new RewardAndBarrier(100,50)
            ,new RewardAndBarrier(125,55)
            ,new RewardAndBarrier(150,60)
            ,new RewardAndBarrier(200,65)
            ,new RewardAndBarrier(300,70)
            ,new RewardAndBarrier(400,75)
            ,new RewardAndBarrier(500,80)};

    public readonly static RewardAndBarrier[] TOTAL_TIMES_YELLOW_BANANA_WAS_SEEN_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(25,20)
            ,new RewardAndBarrier(50,30)
            ,new RewardAndBarrier(100,40)
            ,new RewardAndBarrier(250,50)
            ,new RewardAndBarrier(500,60)
            ,new RewardAndBarrier(750,70)
            ,new RewardAndBarrier(1000,80)
            ,new RewardAndBarrier(1500,90) 
            ,new RewardAndBarrier(2000,100)};

    public readonly static RewardAndBarrier[] TOTAL_TIMES_COCO_WAS_PICKED_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(10,5)
            ,new RewardAndBarrier(30,10)
            ,new RewardAndBarrier(60,15)
            ,new RewardAndBarrier(100,20)
            ,new RewardAndBarrier(150,25)
            ,new RewardAndBarrier(210,30)
            ,new RewardAndBarrier(280,35)
            ,new RewardAndBarrier(360,40)
            ,new RewardAndBarrier(450,45)
            ,new RewardAndBarrier(550,50)
            ,new RewardAndBarrier(660,55)
            ,new RewardAndBarrier(780,60)
            ,new RewardAndBarrier(910,65)
            ,new RewardAndBarrier(1050,70)
            ,new RewardAndBarrier(1200,75)};

    public readonly static RewardAndBarrier[] TOTAL_TIMES_CHOMP_WAS_PICKED_PROGRESSION
     = TOTAL_TIMES_COCO_WAS_PICKED_PROGRESSION;

    public readonly static RewardAndBarrier[] TOTAL_CORRECT_GUESSES_UNDER_HALF_A_SECOND_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(5,25,true)
            ,new RewardAndBarrier(10,50)
            ,new RewardAndBarrier(20,100)};

    public readonly static RewardAndBarrier[] TOTAL_GAMES_WITH_ANDREW_AND_KELSEY_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(10,10,true)
            ,new RewardAndBarrier(25,25)
            ,new RewardAndBarrier(50,50)};

    public readonly static RewardAndBarrier[] TOTAL_GAMES_WITH_COCO_AND_MUFFIN_PROGRESSION
     = TOTAL_GAMES_WITH_ANDREW_AND_KELSEY_PROGRESSION;

    public readonly static RewardAndBarrier[] TOTAL_GAMES_WITH_MONKEY_AND_PENGUIN_PROGRESSION
     = TOTAL_GAMES_WITH_ANDREW_AND_KELSEY_PROGRESSION;

    public readonly static RewardAndBarrier[] TOTAL_CORRECT_GUESSES_STREAK_PROGRESSION
     = new RewardAndBarrier[] {
            new RewardAndBarrier(100,20,true)
            ,new RewardAndBarrier(200,40)
            ,new RewardAndBarrier(400,80)};


    public RewardAndBarrier(float barrier, int reward, bool secret=false)
    {
        Barrier = barrier;
        Reward = reward;
        Secret = secret;
    }
    public float Barrier { get; set; }

    public int Reward { get; set; }
    public bool Secret { get; set; }



}
