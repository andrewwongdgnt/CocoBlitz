﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePage : MonoBehaviour, Page {

    public Text instructions;

    public Slider gameOption;
    public CpuPortrait cpu1Portrait;
    public CpuPortrait cpu2Portrait;
    public CpuPortrait cpu3Portrait;

    public GameUtil.GameModeEnum CurrentGameMode { get;  set; }
    private int pointsToReach;
    private float timer;

    public Sprite player2Sprite;

    public CpuPicker cpuPicker;
    public NavigationArea navArea;
    public GameObject titleArea;
    

    public Toggle singlePlayerToggle;
    public Toggle twoPlayersToggle;

    public MenuAudioManager menuAudioManager;

    List<Cpu> allCpus;

    private int[] cpuIndexes = new int[GameSettingsUtil.MAX_CPU_IN_PLAY_COUNT];
   
    //hack to stop slider sound from playing
    private bool allowGameOptionChangedSound;

    void Start () {
        allCpus = new List<Cpu>() { null, Cpu.KELSEY, Cpu.ANDREW, Cpu.MONKEY, Cpu.PENGUIN };
        allCpus.Add(Cpu.KONGO);
        allCpus.Add(Cpu.PURPLE_MONKEY);
        allCpus.Add(Cpu.MUFFIN);
        allCpus.Add(Cpu.CHOMP);
        allCpus.Add(Cpu.COCO);

        List<int> cpusInPlay = GameSettingsUtil.GetCpusInPlayList();

        SetCpu(0, cpusInPlay[0]);
        SetCpu(1, cpusInPlay[1]);
        SetCpu(2, cpusInPlay[2]);

        cpuPicker.Close(true,false);

        twoPlayersToggle.isOn = GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS;

        allowGameOptionChangedSound = false;
    }
    public void SetActive(bool activate)
    {
        if (CurrentGameMode == GameUtil.GameModeEnum.Coco)
        { 
            int pointsToReach = GameSettingsUtil.GetCocoModePointsToReach();
            gameOption.value = pointsToReach;
        }
        else
        {

            float timer = GameSettingsUtil.GetGoGoModeTimer();
            gameOption.value = timer;
        }
        gameObject.SetActive(activate);
        UpdateGameParams(gameOption.value.ToString());
        titleArea.SetActive(!activate);
    }
    public void GameOptionChanged(float value)
    {
        if (allowGameOptionChangedSound)
            menuAudioManager.PlaySliderOnValueChanged();
        allowGameOptionChangedSound = true;
        UpdateGameParams(value.ToString());
    }

    public void OpenCpuPicker(int cpuPortraitIndex)
    {
        cpuPicker.Open(this,cpuPortraitIndex, cpuIndexes[cpuPortraitIndex], allCpus);
        ShowNavArea(false);
    }
    

    public void SetCpu(int cpuPortraitIndex,int cpuIndex)
    {

        Cpu cpu = allCpus[cpuIndex];

        //Check if cpu is actually available
        if (cpu!=null && cpuIndex!=0 && !GameProgressionUtil.GetCpuAvailability(cpu))
        {
            SetCpu(cpuPortraitIndex, 0);
        }

        cpuIndexes[cpuPortraitIndex] = cpuIndex;


        if (cpu != null && !GameProgressionUtil.GetCpuAvailability(cpu))
            return;

        string name = cpu != null ? cpu.name : Cpu.NO_CPU;
        Sprite sprite = cpu != null ? cpu.sprite : null;
        switch (cpuPortraitIndex)
        {
            case 0:
                cpu1Portrait.SetCpuDisplay(name, sprite);
                break;
            case 1:
                cpu2Portrait.SetCpuDisplay(name, sprite);
                break;
            case 2:
                cpu3Portrait.SetCpuDisplay(name, sprite);
                break;
        }

        GameSettingsUtil.SetCpuInPlay(cpuPortraitIndex, cpuIndex);
    }

    public void ShowNavArea(bool show)
    {
        navArea.gameObject.SetActive(show);
    }

    public void PlayGame()
    {
        GameUtil.currentGameMode = CurrentGameMode;
        GameUtil.pointsToReach = pointsToReach;
        GameUtil.timer = timer;

        GameUtil.cpuList.Clear();
        if (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_SINGLE_PLAYER && cpuIndexes[0] > 0)
        {
            GameUtil.cpuList.Add((Cpu)allCpus[cpuIndexes[0]].RebuildToPlay());
        } 
        if (cpuIndexes[1] > 0)
        {
            GameUtil.cpuList.Add((Cpu)allCpus[cpuIndexes[1]].RebuildToPlay());
        }
        if (cpuIndexes[2] > 0)
        {
            GameUtil.cpuList.Add((Cpu)allCpus[cpuIndexes[2]].RebuildToPlay());
        }

        menuAudioManager.PlayMainButtonClick();

        SceneManager.LoadScene("Game");
    }

    

    void UpdateGameParams( string value)
    {
        if (CurrentGameMode == GameUtil.GameModeEnum.Coco)
        {
            instructions.text = "Get " + value + " correct\nas fast as you can!";
            instructions.fontSize = 80;
            pointsToReach = int.Parse(value);
            GameSettingsUtil.SetCocoModePointsToReach(pointsToReach);
        }
        else
        {
            instructions.text = "Get as many as you can in\n" + value + " seconds!";
            instructions.fontSize = 85;
            timer = float.Parse(value);
            GameSettingsUtil.SetGoGoModeTimer(timer);
        }
    }

    public void SetGameTypeAsSinglePlayer(bool value)
    {
        menuAudioManager.PlayToggleClick();
        if (!value)
        {
            UpdateCpu1PortaitToPlayer2(cpu1Portrait);
        }
        else
        {
            List<int> cpusInPlay = GameSettingsUtil.GetCpusInPlayList();
            SetCpu(0, cpusInPlay[0]);
        }
        GameSettingsUtil.SetGameTypeKey(value ? GameSettingsUtil.GAME_TYPE_SINGLE_PLAYER : GameSettingsUtil.GAME_TYPE_TWO_PLAYERS);
    }

    private void UpdateCpu1PortaitToPlayer2(CpuPortrait cpuPortrait)
    {


        cpuPortrait.button.interactable = false;
        cpuPortrait.portrait.sprite = player2Sprite;
        Color tempColor = cpuPortrait.portrait.color;
        tempColor.a = 1;
        cpuPortrait.portrait.color = tempColor;
        cpuPortrait.nameText.text = Player.PLAYER_2.name;


    }

}
