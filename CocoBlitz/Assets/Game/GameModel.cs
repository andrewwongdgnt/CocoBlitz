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
    private CardManager.EntityEnum? correctEntity = null;
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
        timer = GameManager.currentGameMode == GameManager.GameModeEnum.RackUpThePoints ? GameManager.timer : 0f;
        player.Stats.Restart();
        player.FinalScore = 0;
        player.Points = 0;
        player.Penalties = 0;
        GameManager.cpuList.ForEach(cpu =>
        {
            cpu.Stats.Restart();
            cpu.FinalScore = 0;
            cpu.Points = 0;
            cpu.Penalties = 0;
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

        CardManager.EntityEnum[] allEntities = new CardManager.EntityEnum[CardManager.Entities_2Card.Count];
        CardManager.Entities_2Card.CopyTo(allEntities);

        CardManager.EntityEnum[] currentEntities = card.cardEntities.Select(e => e.entity).ToArray();
        CardManager.EntityEnum[] otherEntities = allEntities.Where(e => !currentEntities.Contains(e)).ToArray();

        bool useCorrectColor = false;
        HashSet<CardManager.ColorEnum> colorsUsed = new HashSet<CardManager.ColorEnum>();
        Dictionary<CardManager.EntityEnum, CardManager.ColorEnum> entityToColor = new Dictionary<CardManager.EntityEnum, CardManager.ColorEnum>();
        Array.ForEach(card.cardEntities, cardEntity => {

            int index = useCorrectColor ? UnityEngine.Random.Range(0, otherEntities.Length) : UnityEngine.Random.Range(0, otherEntities.Length + 1);
            index--;
            CardManager.ColorEnum color;
            do
            {
                if (index < otherEntities.Length)
                    index++;
                else
                    index = 0;

                if (index >= otherEntities.Length)
                    useCorrectColor = true;

                color = CardManager.EntityColorMap[index >= otherEntities.Length ? cardEntity.entity : otherEntities[index]];

            } while (colorsUsed.Contains(color));
            colorsUsed.Add(color);
            entityToColor[cardEntity.entity] = color;
            Debug.Log(cardEntity.entity + ": " + color);

            cardEntity.spriteRenderer.color = CardManager.ColorColorMap[color];

        });

        correctEntity = null;

        List<CardManager.EntityEnum> incorrectEntities = new List<CardManager.EntityEnum>();
        foreach (KeyValuePair<CardManager.EntityEnum, CardManager.ColorEnum> entry in entityToColor)
        {
            CardManager.EntityEnum entity = entry.Key;
            CardManager.ColorEnum color = entry.Value;
            CardManager.EntityEnum otherEntity = CardManager.ColorEntityMap[color];

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
        GameManager.cpuList.ForEach(cpu =>
        {
            cpu.Stats.AddPickedCard(time, card, entityToColor, useCorrectColor);
        });

        //Cpu starts guessing

        player.Guessed = false;
        GameManager.cpuList.ForEach(cpu =>
        {
            cpu.Guessed = false;
        });
        GameManager.cpuList.ForEach(cpu =>
        {
            cpuCoroutines.Add(StartCoroutine(CpuGuess(cpu, useCorrectColor)));
        });
    }

    IEnumerator CpuGuess(Cpu cpu, bool useCorrectColor)
    {
        float delayBeforeAnswer = UnityEngine.Random.Range(cpu.DelayLowerRangeBeforeAnswer, cpu.DelayUpperRangeBeforeAnswer);

        yield return new WaitForSeconds(delayBeforeAnswer);

        CardManager.EntityEnum inCorrectEntity = correctEntity.Value == CardManager.EntityEnum.Banana ? CardManager.EntityEnum.Coco : CardManager.EntityEnum.Banana;

        float randomPercent = UnityEngine.Random.Range(0, 100);
        CardManager.EntityEnum entityToGuess;
        if (useCorrectColor)
        {
            if (cpu.ChanceOfCorrectForCorrectlyColored >= randomPercent)
                entityToGuess = correctEntity.Value;
            else
                entityToGuess = inCorrectEntity;
        }
        else
        {
            if (cpu.ChanceOfCorrectForIncorrectlyColored >= randomPercent)
                entityToGuess = correctEntity.Value;
            else
                entityToGuess = inCorrectEntity;
        }
        Guess(entityToGuess, cpu);
    }

    public void Guess(CardManager.EntityEnum entity, Participant _participant=null)
    {
        if (gameOver || cardInDelay)
            return;

        Participant participant = _participant == null ? player : _participant;
        participant.Guessed = true;
        participant.Stats.AddGuess(Time.time, correctEntity.Value, entity);

        if (entity == correctEntity)
        {
            participant.Points++;
            cpuCoroutines.ForEach(co => StopCoroutine(co));
            cpuCoroutines.Clear();
            if (participant != player && !player.Guessed)
            {
                player.Stats.AddMissed(correctEntity.Value);
            }
            GameManager.cpuList.ForEach(cpu => {
                if (participant != cpu && !cpu.Guessed)
                {
                    cpu.Stats.AddMissed(correctEntity.Value);
                }
            });

        }
        else
        {
            participant.Penalties++;
        }
        participant.FinalScore = participant.Points - participant.Penalties;
        UpdateScoresText();

        CheckForGameOver();
        if (!gameOver && (entity == correctEntity || (player.Guessed && GameManager.cpuList.All(cpu => cpu.Guessed))))
        {
            StartCoroutine(NewRoundWithDelay());
            
        }
    }

    IEnumerator NewRoundWithDelay()
    {
        cardInDelay = true;
        float totalDelay = SettingsManager.GetCardDelay();

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
        playerScoreText.text = player.FinalScore.ToString();
        cpu1ScoreText.text = GameManager.cpuList.Count > 0 ? GameManager.cpuList[0].FinalScore.ToString() : "-";
        cpu2ScoreText.text = GameManager.cpuList.Count > 1 ? GameManager.cpuList[1].FinalScore.ToString() : "-";
        cpu3ScoreText.text = GameManager.cpuList.Count > 2 ? GameManager.cpuList[2].FinalScore.ToString() : "-";
    }

    private void CheckForGameOver()
    {
        if (GameManager.currentGameMode == GameManager.GameModeEnum.FastestTime)
        {
            if (player.FinalScore >= GameManager.pointsToReach || GameManager.cpuList.Any(cpu => cpu.FinalScore>= GameManager.pointsToReach))
                gameOver = true;
        }
        else if (GameManager.currentGameMode == GameManager.GameModeEnum.RackUpThePoints)
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
            if (GameManager.currentGameMode == GameManager.GameModeEnum.FastestTime)
            {
                timer += Time.deltaTime;
            }
            else if (GameManager.currentGameMode == GameManager.GameModeEnum.RackUpThePoints)
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
        statsList.AddRange(GameManager.cpuList.Select(cpu => cpu.Stats));
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
