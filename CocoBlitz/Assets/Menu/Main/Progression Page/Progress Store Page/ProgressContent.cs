using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressContent : MonoBehaviour {

    public ProgressBar gamesPlayedProgressBar;

	// Use this for initialization
	void Start () {
        gamesPlayedProgressBar.SetValue(9,10);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
