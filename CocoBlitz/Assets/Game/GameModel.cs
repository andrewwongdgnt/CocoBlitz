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
    public Text playerNameText;
    public Text cpu1ScoreText;
    public Text cpu1NameText;
    public Text cpu2ScoreText;
    public Text cpu2NameText;
    public Text cpu3ScoreText;
    public Text cpu3NameText;
    public Text timerText;
    public PauseMenu pauseMenu;
    public GameObject roundSeperator;
    public Text delayValue;

    public UCWManager ucwManager;

    private Player player1;
    private Player player2;
    private CardUtil.EntityEnum? correctEntity = null;

    private float timer;

    private bool gameOver;
    private bool showingGameOverMenu;


    private List<Coroutine> cpuCoroutines = new List<Coroutine>();

    private bool cardInDelay;

    private bool kongoInitialAvailability;
    private bool purpleMonkeyInitialAvailability;
    private bool muffinInitialAvailability;
    private bool chompInitialAvailability;
    private bool cocoInitialAvailability;

    // Use this for initialization
    void Start () {
       
        Debug.Log("Game Begins");

        ucwManager.Hide();
        kongoInitialAvailability = GameProgressionUtil.GetCpuAvailability(Cpu.KONGO);
        purpleMonkeyInitialAvailability = GameProgressionUtil.GetCpuAvailability(Cpu.PURPLE_MONKEY);
        muffinInitialAvailability = GameProgressionUtil.GetCpuAvailability(Cpu.MUFFIN);
        chompInitialAvailability = GameProgressionUtil.GetCpuAvailability(Cpu.CHOMP);
        cocoInitialAvailability = GameProgressionUtil.GetCpuAvailability(Cpu.COCO);

        showingGameOverMenu = false;
        cpuCoroutines.Clear();
        timer = GameUtil.currentGameMode == GameUtil.GameModeEnum.GoGo ? GameUtil.timer : 0f;

        player1 = (Player)Player.PLAYER_1.RebuildToPlay();
        player1.Stats.Restart();
        player1.finalScore = 0;
        player1.points = 0;
        player1.penalties = 0;
        if (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_SINGLE_PLAYER)
            player1.SetNewName(Player.SINGLE_PLAYER_NAME);

        player2 = (Player)Player.PLAYER_2.RebuildToPlay();
        if (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS)
        {
            player2.Stats.Restart();
            player2.finalScore = 0;
            player2.points = 0;
            player2.penalties = 0;
        }
        GameUtil.cpuList.ForEach(cpu =>
        {
            cpu.Stats.Restart();
            cpu.finalScore = 0;
            cpu.points = 0;
            cpu.penalties = 0;
        });
        UpdateScoresAndNameText();
        gameOver = false;
        cardInDelay = false;
        roundSeperator.SetActive(false);
        Array.ForEach(cards_2Entities, c => c.gameObject.SetActive(false));
        StartCoroutine(NewRoundWithDelay());
    }
    

    private void NewRound()
    {

        //Generate Card
        Debug.Log("Generating card");
        Card card = cards_2Entities[UnityEngine.Random.Range(0, cards_2Entities.Length)];

        Array.ForEach(cards_2Entities, c => c.gameObject.SetActive(c == card));

        CardUtil.EntityEnum[] allEntities = new CardUtil.EntityEnum[CardUtil.Entities_2Card.Count];
        CardUtil.Entities_2Card.CopyTo(allEntities);

        CardUtil.EntityEnum[] currentEntities = card.cardEntities.Select(e => e.entity).ToArray();
        CardUtil.EntityEnum[] otherEntities = allEntities.Where(e => !currentEntities.Contains(e)).ToArray();

        if (currentEntities.Contains(CardUtil.EntityEnum.Chomp))
        {
            GameProgressionUtil.IncrementTotalCountOfWhenChompWasSeen();
        }

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

        player1.Stats.AddPickedCard(time, card, entityToColor, useCorrectColor);
        player1.guessed = false;
        if (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS)
        {
            player2.Stats.AddPickedCard(time, card, entityToColor, useCorrectColor);
            player2.guessed = false;
        }

        GameUtil.cpuList.ForEach(cpu =>
        {
            cpu.Stats.AddPickedCard(time, card, entityToColor, useCorrectColor);
        });
        GameUtil.cpuList.ForEach(cpu =>
        {
            cpu.guessed = false;
        });

        //Cpu starts guessing
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

    public void GuessForPlayer2(CardUtil.EntityEnum entity)
    {
        Guess(entity, player2);
    }

    public void Guess(CardUtil.EntityEnum entity, Participant _participant=null)
    {
        if (gameOver || cardInDelay)
            return;

        Participant participant = _participant == null ? player1 : _participant;
        if (participant.guessed)
            return;

        participant.guessed = true;
        float timeToGuess = participant.Stats.AddGuess(Time.time, correctEntity.Value, entity);

        if (entity == correctEntity)
        {
            participant.points++;

            //Clear coroutine so other cpus will stop guessing
            cpuCoroutines.ForEach(co => StopCoroutine(co));
            cpuCoroutines.Clear();

            if (participant != player1 && !player1.guessed)
            {
                player1.Stats.AddMissed(correctEntity.Value);
            }
            if (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS && participant != player2 && !player2.guessed)
            {
                player2.Stats.AddMissed(correctEntity.Value);
            }
            GameUtil.cpuList.ForEach(cpu => {
                if (participant != cpu && !cpu.guessed)
                {
                    cpu.Stats.AddMissed(correctEntity.Value);
                }
            });

            if (participant == player1 || (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS && participant == player2))
            {
                if (correctEntity == CardUtil.EntityEnum.Coco)
                { 
                    GameProgressionUtil.IncrementTotalCountOfWhenCocoWasCorrectlyPicked();
                }
                if (timeToGuess < 1f)
                {
                    GameProgressionUtil.IncrementTotalCountOfCorrectGuessUnderOneSecond();
                }
            }

        }
        else if (SettingsUtil.IsPenaltiesAllowed())
        {
            participant.penalties++;
            if (participant.points - participant.penalties<0 && !SettingsUtil.IsNegativeScoresAllowed())
            {
                participant.penalties--;
            }
        }
        
        participant.finalScore = participant.points - participant.penalties;
        UpdateScoresAndNameText();

        CheckForGameOver();
        bool player2Guessed = GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS && player2.guessed;
        if (!gameOver && (entity == correctEntity || ((GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_SINGLE_PLAYER || player2Guessed) && player1.guessed && GameUtil.cpuList.All(cpu => cpu.guessed))))
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

    private void UpdateScoresAndNameText()
    {
        playerScoreText.text = player1.finalScore.ToString();
        playerNameText.text = player1.name;

        string cpu1Score = GameUtil.cpuList.Count > 0 ? GameUtil.cpuList[0].finalScore.ToString() : "-";
        string cpu2Score = GameUtil.cpuList.Count > 1 ? GameUtil.cpuList[1].finalScore.ToString() : "-";
        string cpu3Score = GameUtil.cpuList.Count > 2 ? GameUtil.cpuList[2].finalScore.ToString() : "-";

        if (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS)
        {
            cpu1ScoreText.text = player2.finalScore.ToString();
            cpu1NameText.text = player2.name;
            cpu2ScoreText.text = cpu1Score;
            cpu2NameText.text = GameUtil.cpuList.Count > 0 ? GameUtil.cpuList[0].name : "";
            cpu3ScoreText.text = cpu2Score;
            cpu3NameText.text = GameUtil.cpuList.Count > 1 ? GameUtil.cpuList[1].name : "";
        }
        else
        {
            cpu1ScoreText.text =  cpu1Score;
            cpu1NameText.text = GameUtil.cpuList.Count > 0 ? GameUtil.cpuList[0].name : "";
            cpu2ScoreText.text = cpu2Score;
            cpu2NameText.text = GameUtil.cpuList.Count > 1 ? GameUtil.cpuList[1].name : "";
            cpu3ScoreText.text = cpu3Score;
            cpu3NameText.text = GameUtil.cpuList.Count > 2 ? GameUtil.cpuList[2].name : "";
        }
    }

    private void CheckForGameOver()
    {
        if (GameUtil.currentGameMode == GameUtil.GameModeEnum.Coco)
        {
            if (player1.finalScore >= GameUtil.pointsToReach 
                || (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS && player2.finalScore >= GameUtil.pointsToReach)
                || GameUtil.cpuList.Any(cpu => cpu.finalScore>= GameUtil.pointsToReach))
                gameOver = true;
        }
        else if (GameUtil.currentGameMode == GameUtil.GameModeEnum.GoGo && timer <= 0)
        {
            gameOver = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!cardInDelay && !gameOver)
        {
            if (GameUtil.currentGameMode == GameUtil.GameModeEnum.Coco)
            {
                timer += Time.deltaTime;
            }
            else if (GameUtil.currentGameMode == GameUtil.GameModeEnum.GoGo)
            {
                timer -= Time.deltaTime;
            }
        }
        
        timerText.text = TransformTime(timer);
        CheckForGameOver();
        if (!gameOver)
        {
            timerText.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            timerText.color = new Color32(0, 255, 0, 255);
        }
        if (gameOver && !showingGameOverMenu)
        {
            GameProgressionUtil.IncrementTotalGamesPlayed();
            GameProgressionUtil.IncreaseTimeSpent(GameUtil.currentGameMode == GameUtil.GameModeEnum.GoGo ? GameUtil.timer : timer);
            showingGameOverMenu = true;
            ShowGameOverMenu();

            //Start unlocking
            AttemptCpuUnlock(kongoInitialAvailability, Cpu.KONGO);
            AttemptCpuUnlock(purpleMonkeyInitialAvailability, Cpu.PURPLE_MONKEY);
            AttemptCpuUnlock(muffinInitialAvailability, Cpu.MUFFIN);
            AttemptCpuUnlock(chompInitialAvailability, Cpu.CHOMP);
            AttemptCpuUnlock(cocoInitialAvailability, Cpu.COCO);
            ucwManager.BeginUnlockPhase();


        }

       
    }

    private void AttemptCpuUnlock(bool initialAvailability, Cpu cpu)
    {    

        if (initialAvailability)
        {
            return;
        }
        if (GameProgressionUtil.GetCpuAvailability(cpu))
        {
            ucwManager.AddCpuToUnlock(cpu);
        }
    }

    private void ShowGameOverMenu()
    {
        List<Statistics> statsList = new List<Statistics>();
        statsList.Add(player1.Stats);
        if (GameSettingsUtil.GetGameTypeKey() == GameSettingsUtil.GAME_TYPE_TWO_PLAYERS)
        {
            statsList.Add(player2.Stats);
        }
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
