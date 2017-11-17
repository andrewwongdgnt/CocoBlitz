using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePage : MonoBehaviour, Page {

    public Text instructions;
    public Dropdown gameOptions;
    
    public Dropdown cpu1Options;
    public Dropdown cpu2Options;
    public Dropdown cpu3Options;

    public GameManager.GameModeEnum CurrentGameMode { get;  set; }
    private int pointsToReach;
    private float timer;


    List<string> cpuNames;
    // Use this for initialization
    void Start () {
        cpuNames = new List<string>() { "None", "Kelsey", "Andrew", "Muffin" };
        cpu1Options.AddOptions(cpuNames);
        cpu2Options.AddOptions(cpuNames);
        cpu3Options.AddOptions(cpuNames);
    }
    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
        UpdateGameParams(gameOptions.options[gameOptions.value].text);
    }
    public void DropDownChanged(int i)
    {
        string value = gameOptions.options[i].text;
        UpdateGameParams(value);
    }

    public void PlayGame()
    {
        GameManager.currentGameMode = CurrentGameMode;
        GameManager.pointsToReach = pointsToReach;
        GameManager.timer = timer;

        GameManager.cpuList.Clear();
        if (cpu1Options.value > 0)
        {
            GameManager.cpuList.Add(GetCpu(cpu1Options.value));
        }
        if (cpu2Options.value > 0)
        {
            GameManager.cpuList.Add(GetCpu(cpu2Options.value));
        }
        if (cpu3Options.value > 0)
        {
            GameManager.cpuList.Add(GetCpu(cpu3Options.value));
        }

        SceneManager.LoadScene("Game");
    }



    Cpu GetCpu(int index)
    {
        switch (index)
        {
            case 1:
                return Cpu.KELSEY;
            case 2:
                return Cpu.ANDREW;
            case 3:
                return Cpu.MUFFIN;
            default:
                return null;
        }
    }

    void UpdateGameParams( string value)
    {
        if (CurrentGameMode == GameManager.GameModeEnum.FastestTime)
        {
            instructions.text = "Get " + value + " correct as fast as you can!";
            instructions.fontSize = 100;
            pointsToReach = int.Parse(value);
        }
        else
        {
            instructions.text = "Get as many correct as possible within " + value + " seconds!";
            instructions.fontSize = 85;
            timer = float.Parse(value);
        }
    }
}
