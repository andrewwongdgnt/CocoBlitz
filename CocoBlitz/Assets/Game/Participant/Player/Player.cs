using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Participant {
    public static readonly Player PLAYER_1 = new Player("Player 1");
    public static readonly Player PLAYER_2 = new Player("Player 2");
    public Player(string name) : base(name)
    {

    }


}
