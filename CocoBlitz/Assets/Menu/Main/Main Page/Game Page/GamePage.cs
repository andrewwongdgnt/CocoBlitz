using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePage : MonoBehaviour, Page {

    public Text instructions;
    public Dropdown gameOptions;

    public CpuPortrait cpu1Portrait;
    public CpuPortrait cpu2Portrait;
    public CpuPortrait cpu3Portrait;

    public GameUtil.GameModeEnum CurrentGameMode { get;  set; }
    private int pointsToReach;
    private float timer;

    public Sprite kelseySprite;
    public Sprite andrewSprite;
    public Sprite monkeySprite;
    public Sprite penguinSprite;
    public Sprite kongoSprite;
    public Sprite purpleMonkeySprite;
    public Sprite muffinSprite;
    public Sprite chompSprite;
    public Sprite cocoSprite;

    public CpuPicker cpuPicker;
    public NavigationArea navArea;
    public GameObject titleArea;

    List<Cpu> allCpus;

    private int[] cpuIndexes = new int[3];
    // Use this for initialization
    void Start () {
        allCpus = new List<Cpu>() { null, Cpu.KELSEY, Cpu.ANDREW, Cpu.MONKEY, Cpu.PENGUIN };
        allCpus.Add(Cpu.KONGO);
        allCpus.Add(Cpu.PURPLE_MONKEY);
        allCpus.Add(Cpu.MUFFIN);
        allCpus.Add(Cpu.CHOMP);
        allCpus.Add(Cpu.COCO);

        Cpu.KELSEY.sprite = kelseySprite;
        Cpu.ANDREW.sprite = andrewSprite;
        Cpu.MONKEY.sprite = monkeySprite;
        Cpu.PENGUIN.sprite = penguinSprite;
        Cpu.KONGO.sprite = kongoSprite;
        Cpu.PURPLE_MONKEY.sprite = purpleMonkeySprite;
        Cpu.MUFFIN.sprite = muffinSprite;
        Cpu.CHOMP.sprite = chompSprite;
        Cpu.COCO.sprite = cocoSprite;


        cpu1Portrait.SetCpuDisplay("None", null);
        cpu2Portrait.SetCpuDisplay("None", null);
        cpu3Portrait.SetCpuDisplay("None", null);

        cpuPicker.Close(true);
    }
    public void SetActive(bool activate)
    {

        gameObject.SetActive(activate);
        UpdateGameParams(gameOptions.options[gameOptions.value].text);
        titleArea.SetActive(!activate);
    }
    public void DropDownChanged(int i)
    {
        string value = gameOptions.options[i].text;
        UpdateGameParams(value);
    }

    public void OpenCpuPicker(int cpuPortraitIndex)
    {
        cpuPicker.Open(this,cpuPortraitIndex, cpuIndexes[cpuPortraitIndex], allCpus);
        ShowNavArea(false);
    }

    public void UpdateCpuPortrait(int cpuPortraitIndex,int cpuIndex)
    {
        cpuIndexes[cpuPortraitIndex] = cpuIndex;

        Cpu cpu = allCpus[cpuIndex];
        string name = cpu != null ? cpu.name : "None";
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
        if (cpuIndexes[0] > 0)
        {
            GameUtil.cpuList.Add(allCpus[cpuIndexes[0]]);
        }
        if (cpuIndexes[1] > 0)
        {
            GameUtil.cpuList.Add(allCpus[cpuIndexes[1]]);
        }
        if (cpuIndexes[2] > 0)
        {
            GameUtil.cpuList.Add(allCpus[cpuIndexes[2]]);
        }

        SceneManager.LoadScene("Game");
    }

    

    void UpdateGameParams( string value)
    {
        if (CurrentGameMode == GameUtil.GameModeEnum.FastestTime)
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
