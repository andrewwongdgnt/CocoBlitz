using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Participant  {


    public int points { get; set; }
    public int penalties { get; set; }
    public int finalScore { get; set; }

    public string name { get; protected set; }
    public bool guessed { get; set; }

    public Statistics Stats { get; private set; }

    public Participant (string name)
    {
        this.name = name;
        Stats = new Statistics(name);
    }

    public abstract Participant RebuildToPlay();



}
