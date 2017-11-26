using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModel : MonoBehaviour {

    public Card[] cards_2Entities;
    public Text playerScoreText;
    public Text cpu1ScoreText;
    public Text cpu2ScoreText;
    public Text cpu3ScoreText;
    public Text timerText;
    public PauseMenu pauseMenu;
    public GameObject roundSeperator;
    public Text delayValue;

    private Player player;
    private CardUtil.EntityEnum? correctEntity = null;
    private GameObject[] cardGameObjects;

    private float timer;

    private bool gameOver;
    private bool showingGameOverMenu;


    private List<Coroutine> cpuCoroutines = new List<Coroutine>();

    private bool cardInDelay;

    // Use this for initialization
    void Start () {
        cardGameObjects = GameObject.FindGameObjectsWithTag("Card");
        player = new Player();
       

        Begin();


    }

    private void Begin()
    {
        Debug.Log("Game Begins");
        showingGameOverMenu = false;
        cpuCoroutines.Clear();
        timer = GameUtil.currentGameMode == GameUtil.GameModeEnum.RackUpThePoints ? GameUtil.timer : 0f;
        player.Stats.Restart();
        player.finalScore = 0;
        player.points = 0;
        player.penalties = 0;
        GameUtil.cpuList.ForEach(cpu =>
        {
            cpu.Stats.Restart();
            cpu.finalScore = 0;
            cpu.points = 0;
            cpu.penalties = 0;
        });
        UpdateScoresText();
        gameOver = false;
        cardInDelay = false;
        roundSeperator.SetActive(false);
        NewRound();
    }
    

    private void NewRound()
    {

        //Generate Card
        Debug.Log("Generating card");
        Card card = cards_2Entities[UnityEngine.Random.Range(0, cards_2Entities.Length)];
        
        Array.ForEach(cardGameObjects, ent => ent.SetActive(ent.GetComponent<Card>() == card));

        CardUtil.EntityEnum[] allEntities = new CardUtil.EntityEnum[CardUtil.Entities_2Card.Count];
        CardUtil.Entities_2Card.CopyTo(allEntities);

        CardUtil.EntityEnum[] currentEntities = card.cardEntities.Select(e => e.entity).ToArray();
        CardUtil.EntityEnum[] otherEntities = allEntities.Where(e => !currentEntities.Contains(e)).ToArray();

        bool useCorrectColor = false;
        HashSet<CardUtil.ColorEnum> colorsUsed = new HashSet<CardUtil.ColorEnum>();
        Dictionary<CardUtil.EntityEnum, CardUtil.ColorEnum> entityToColor = new Dictionary<CardUtil.EntityEnum, CardUtil.ColorEnum>();
        Array.ForEach(card.cardEntities, cardEntity => {

            int index = useCorrectColor ? UnityEngine.Random.Range(0, otherEntities.Length) : UnityEngine.Random.Range(0, otherEntities.Length + 1);
            index--;
            CardUtil.ColorEnum color;
            do
            {
                if (index < otherEntities.Length)
                    index++;
                else
                    index = 0;

                if (index >= otherEntities.Length)
                    useCorrectColor = true;

                color = CardUtil.EntityColorMap[index >= otherEntities.Length ? cardEntity.entity : otherEntities[index]];

            } while (colorsUsed.Contains(color));
            colorsUsed.Add(color);
            entityToColor[cardEntity.entity] = color;
            Debug.Log(cardEntity.entity + ": " + color);

            cardEntity.spriteRenderer.color = CardUtil.ColorColorMap[color];

        });

        correctEntity = null;

        List<CardUtil.EntityEnum> incorrectEntities = new List<CardUtil.EntityEnum>();
        foreach (KeyValuePair<CardUtil.EntityEnum, CardUtil.ColorEnum> entry in entityToColor)
        {
            CardUtil.EntityEnum entity = entry.Key;
            CardUtil.ColorEnum color = entry.Value;
            CardUtil.EntityEnum otherEntity = CardUtil.ColorEntityMap[color];

            if (entity == otherEntity)
            {
                correctEntity = entity;
                break;
            }

            incorrectEntities.Add(entity);
            incorrectEntities.Add(otherEntity);

        }

        if (correctEntity == null)
            correctEntity = allEntities.Except(incorrectEntities).First();

        //Set up stats

        float time = Time.time;

        player.Stats.AddPickedCard(time, card, entityToColor, useCorrectColor);
        GameUtil.cpuList.ForEach(cpu =>
        {
            cpu.Stats.AddPickedCard(time, card, entityToColor, useCorrectColor);
        });

        //Cpu starts guessing

        player.guessed = false;
        GameUtil.cpuList.ForEach(cpu =>
        {
            cpu.guessed = false;
        });
        GameUtil.cpuList.ForEach(cpu =>
        {
            cpuCoroutines.Add(StartCoroutine(CpuGuess(cpu, useCorrectColor)));
        });
    }

    IEnumerator CpuGuess(Cpu cpu, bool useCorrectColor)
    {
        float delayModifier = 0;
        if (cpu.delayModiferDict != null)
        {
            cpu.delayModiferDict.TryGetValue(correctEntity.Value, out delayModifier);
        }

        float delayBeforeAnswer = UnityEngine.Random.Range(cpu.delayLowerRangeBeforeAnswer + delayModifier, cpu.delayUpperRangeBeforeAnswer + delayModifier);

        yield return new WaitForSeconds(delayBeforeAnswer);

        CardUtil.EntityEnum inCorrectEntity = correctEntity.Value == CardUtil.EntityEnum.Banana ? CardUtil.EntityEnum.Coco : CardUtil.EntityEnum.Banana;

        float chanceOfCorrectModifier = 0;
        if (cpu.chanceOfCorrectModiferDict != null)
        {
            cpu.chanceOfCorrectModiferDict.TryGetValue(correctEntity.Value, out chanceOfCorrectModifier);
        }

        float randomPercent = UnityEngine.Random.Range(0, 100);
        CardUtil.EntityEnum entityToGuess;
        if (useCorrectColor)
        {
            if (cpu.chanceOfCorrectForCorrectlyColored + chanceOfCorrectModifier >= randomPercent)
                entityToGuess = correctEntity.Value;
            else
                entityToGuess = inCorrectEntity;
        }
        else
        {
            if (cpu.chanceOfCorrectForIncorrectlyColored + chanceOfCorrectModifier >= randomPercent)
                entityToGuess = correctEntity.Value;
            else
                entityToGuess = inCorrectEntity;
        }
        Guess(entityToGuess, cpu);
    }

    public void Guess(CardUtil.EntityEnum entity, Participant _participant=null)
    {
        if (gameOver || cardInDelay)
            return;

        Participant participant = _participant == null ? player : _participant;
        participant.guessed = true;
        participant.Stats.AddGuess(Time.time, correctEntity.Value, entity);

        if (entity == correctEntity)
        {
            participant.points++;
            cpuCoroutines.ForEach(co => StopCoroutine(co));
            cpuCoroutines.Clear();
            if (participant != player && !player.guessed)
            {
                player.Stats.AddMissed(correctEntity.Value);
            }
            GameUtil.cpuList.ForEach(cpu => {
                if (participant != cpu && !cpu.guessed)
                {
                    cpu.Stats.AddMissed(correctEntity.Value);
                }
            });

        }
        else
        {
            participant.penalties++;
        }
        participant.finalScore = participant.points - participant.penalties;
        UpdateScoresText();

        CheckForGameOver();
        if (!gameOver && (entity == correctEntity || (player.guessed && GameUtil.cpuList.All(cpu => cpu.guessed))))
        {
            StartCoroutine(NewRoundWithDelay());
            
        }
    }

    IEnumerator NewRoundWithDelay()
    {
        cardInDelay = true;
        float totalDelay = SettingsUtil.GetCardDelay();

        roundSeperator.SetActive(totalDelay>0.25f);

        delayValue.text = "3";
        yield return new WaitForSeconds(totalDelay / 3);
        delayValue.text = "2";
        yield return new WaitForSeconds(totalDelay / 3);
        delayValue.text = "1";
        yield return new WaitForSeconds(totalDelay / 3);

        roundSeperator.SetActive(false);

        NewRound();
        cardInDelay = false;
    }

    private void UpdateScoresText()
    {
        playerScoreText.text = player.finalScore.ToString();
        cpu1ScoreText.text = GameUtil.cpuList.Count > 0 ? GameUtil.cpuList[0].finalScore.ToString() : "-";
        cpu2ScoreText.text = GameUtil.cpuList.Count > 1 ? GameUtil.cpuList[1].finalScore.ToString() : "-";
        cpu3ScoreText.text = GameUtil.cpuList.Count > 2 ? GameUtil.cpuList[2].finalScore.ToString() : "-";
    }

    private void CheckForGameOver()
    {
        if (GameUtil.currentGameMode == GameUtil.GameModeEnum.FastestTime)
        {
            if (player.finalScore >= GameUtil.pointsToReach || GameUtil.cpuList.Any(cpu => cpu.finalScore>= GameUtil.pointsToReach))
                gameOver = true;
        }
        else if (GameUtil.currentGameMode == GameUtil.GameModeEnum.RackUpThePoints)
        {
            if (timer <= 0)
                gameOver = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!cardInDelay && !gameOver)
        {
            if (GameUtil.currentGameMode == GameUtil.GameModeEnum.FastestTime)
            {
                timer += Time.deltaTime;
            }
            else if (GameUtil.currentGameMode == GameUtil.GameModeEnum.RackUpThePoints)
            {
                timer -= Time.deltaTime;
            }
        }
        
        timerText.text = TransformTime(timer);
        CheckForGameOver();
        if (!gameOver)
        {
            timerText.color = new Color32(0, 0, 0, 255);
        }
        else
        {
            timerText.color = new Color32(0, 255, 0, 255);
        }
        if (gameOver && !showingGameOverMenu)
        {
            showingGameOverMenu = true;
            ShowGameOverMenu();
        }

       
    }

    private void ShowGameOverMenu()
    {
        List<Statistics> statsList = new List<Statistics>();
        statsList.Add(player.Stats);
        statsList.AddRange(GameUtil.cpuList.Select(cpu => cpu.Stats));
        pauseMenu.GameOver(statsList);
    }

    private string TransformTime(float timer)
    {
        if (timer < 0)
            return TransformTime(0);
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        int milliseconds = Mathf.FloorToInt(timer*10)- Mathf.FloorToInt(timer )*10;
        string niceTime = string.Format("{0:0}:{1:00}.{2:0}", minutes, seconds, milliseconds);

        return niceTime;
    }


}
