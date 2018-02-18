using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCWManager : MonoBehaviour {

    private Stack<Cpu> cpusToUnlock = new Stack<Cpu>();
    public UCW ucw;
	// Use this for initialization
	void Start () {
        cpusToUnlock.Clear();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Unlock()
    {
        AttemptUnlock();
    }

    public void AddCpuToUnlock(Cpu cpu)
    {
        cpusToUnlock.Push(cpu);
    }

    public void BeginUnlockPhase()
    {
        Show();
        AttemptUnlock();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {

        gameObject.SetActive(true);
    }

    private void AttemptUnlock()
    {
        if (cpusToUnlock.Count > 0)
        {
            ucw.Show();
            Cpu cpu = cpusToUnlock.Pop();

            ucw.SetCharacterSprite(cpu.sprite);
            ucw.SetCharacterName(cpu.name);

        } else
        {
            ucw.Hide();
        }
    }
}
