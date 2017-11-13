using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant  {


    public int Points { get; set; }
    public int Penalties { get; set; }
    public int FinalScore { get; set; }

    public string Name { get; private set; }
    public bool Guessed { get; set; }

    public Statistics Stats { get; private set; }

    public Participant (string name)
    {
        Name = name;
        Stats = new Statistics(name);
    }

}
