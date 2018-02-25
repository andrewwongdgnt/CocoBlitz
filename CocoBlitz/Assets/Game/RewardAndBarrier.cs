

using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardAndBarrier
{



    public readonly static RewardAndBarrier[] TOTAL_GAMES_PLAYED_PROGRESSION
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


    public RewardAndBarrier(float barrier, int reward)
    {
        Barrier = barrier;
        Reward = reward;
    }
    public float Barrier { get; set; }

    public int Reward { get; set; }



}
