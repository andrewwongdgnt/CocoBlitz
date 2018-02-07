using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPage : MonoBehaviour, Page
{

	// Use this for initialization
	void Start () {
		
	}

    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
    }
}
