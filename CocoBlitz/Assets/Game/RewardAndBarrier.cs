

using System;
using System.Collections.Generic;
using UnityEngine;

public class RewardAndBarrier {


    public readonly static RewardAndBarrier TOTAL_GAMES_PLAYED_PROGRESSION
        = new RewardAndBarrier(new RewardAndBarrier.Container[] {
            new RewardAndBarrier.Container(10,5)
            ,new RewardAndBarrier.Container(30,10)
            ,new RewardAndBarrier.Container(60,15)
            ,new RewardAndBarrier.Container(100,20)
            ,new RewardAndBarrier.Container(150,25)
            ,new RewardAndBarrier.Container(210,30)
            ,new RewardAndBarrier.Container(280,35)
            ,new RewardAndBarrier.Container(360,40)
            ,new RewardAndBarrier.Container(450,45)
            ,new RewardAndBarrier.Container(550,50)
            ,new RewardAndBarrier.Container(660,55)
            ,new RewardAndBarrier.Container(780,60)
            ,new RewardAndBarrier.Container(910,65)
            ,new RewardAndBarrier.Container(1050,70)
            ,new RewardAndBarrier.Container(1200,75)});

    public class Container

    {
        public Container(float barrier, int reward)
        {
            Barrier = barrier;
            Reward = reward;
        }
        public float Barrier { get; set; }

        public int Reward { get; set; }
    }

    public Container[] RewardAndBarriers {  get; private set; }
    public RewardAndBarrier(Container[] rewardAndBarriers)
    {
        RewardAndBarriers = rewardAndBarriers;
    }


}
