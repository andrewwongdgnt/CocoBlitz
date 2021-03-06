﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModel : MonoBehaviour
{

    private readonly static float MIN_ROUND_DELAY_TO_DISPLAY_ROUND_SEP = 0.25f;

    public GameAudioManager gameAudioManager;
    public GameObject cardContainer;
    public Card[] cards_2Entities;
    public Card[] secretCards_2Entities;
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

    public Button[] player1Buttons;
    public Button[] player2Buttons;

    public BananaRewardWindowManager bananaRewardWindowManager;

    private List<Card> allCards;

    private Player player1;
    private Player player2;
    private CardUtil.EntityEnum? correctEntity = null;
    private bool useCorrectColor;

    private float timer;

    private bool gameOver;
    private bool showingGameOverMenu;


    private List<Coroutine> cpuCoroutines = new List<Coroutine>();

    private bool cardInDelay;

    private int correctStreak;

    private Card cardGameObject;

    private class GameProgressionLogicContainer
    {
        public float initialValue;
        public Func<GameProgressionRepresentation, float> lambda;
        public RewardAndBarrier[] rewardAndBarriers;
        public string reasonString;

        public GameProgressionLogicContainer(float initialValue, Func<GameProgressionRepresentation, float> lambda, RewardAndBarrier[] rewardAndBarriers, string reasonString)
        {
            this.initialValue = initialValue;
            this.lambda = lambda;
            this.rewardAndBarriers = rewardAndBarriers;
            this.reasonString = reasonString;
        }
    }

    List<GameProgressionLogicContainer> gameProgressionLogicContainerList = new List<GameProgressionLogicContainer>();

    // Use this for initialization
    void Start()
    {

        Debug.Log("Game Begins");

        bananaRewardWindowManager.Hide();

        BuildGameProgressionLogicList();

        //Building card list including secret cards
        allCards = new List<Card>();
        allCards.AddRange(cards_2Entities);
        Array.ForEach(secretCards_2Entities, card =>
        {

            if (GameProgressionUtil.GetCardAvailability(card.cardEnum)){
                allCards.Add(card);
            }
        });

        showingGameOverMenu = false;
        cpuCoroutines.Clear();
        timer = GameSettingsUtil.GetGameMode() == GameSettingsUtil.GameModeEnum.GoGo ? GameUtil.timer : 0f;

        player1 = (Player)Player.PLAYER_1.RebuildToPlay();
        player1.Stats.Restart();
        player1.finalScore = 0;
        player1.points = 0;
        player1.penalties = 0;
        if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Single)
            player1.SetNewName(Player.SINGLE_PLAYER_NAME);

        player2 = (Player)Player.PLAYER_2.RebuildToPlay();
        if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two)
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
        StartCoroutine(NewRoundWithDelay());
    }

    private void BuildGameProgressionLogicList()
    {
        BuildGameProgressionLogicList(rep => rep.totalSinglePlayerGamesPlayed
        , RewardAndBarrier.TOTAL_SINGLE_PLAYER_GAMES_PLAYED_PROGRESSION
        , "Play {0} single player games");

        BuildGameProgressionLogicList(rep => rep.totalTwoPlayerGamesPlayed
        , RewardAndBarrier.TOTAL_TWO_PLAYER_GAMES_PLAYED_PROGRESSION
        , "Play {0} two player games");

        BuildGameProgressionLogicList(rep => rep.totalTimeSpentPlaying
        , RewardAndBarrier.TOTAL_TIME_SPENT_PLAYING_PROGRESSION
        , "Play for {0} seconds");

        BuildGameProgressionLogicList(rep => rep.totalCpusDefeated
        , RewardAndBarrier.TOTAL_CPUS_DEFEATED_PROGRESSION
        , "Defeat {0} CPUs");

        BuildGameProgressionLogicList(rep => rep.totalCorrectCorrectlyColoredGuesses
        , RewardAndBarrier.TOTAL_CORRECT_CORRECTLY_COLORED_GUESSES_PROGRESSION
        , "Guess {0} items correctly (correctly colored)");

        BuildGameProgressionLogicList(rep => rep.totalCorrectIncorrectlyColoredGuesses
        , RewardAndBarrier.TOTAL_CORRECT_INCORRECTLY_COLORED_GUESSES_PROGRESSION
        , "Guess {0} items correctly (incorrectly colored)");

        BuildGameProgressionLogicList(rep => rep.totalCorrectGuessesUnderOneSecond
        , RewardAndBarrier.TOTAL_CORRECT_GUESSES_UNDER_ONE_SECOND_PROGRESSION
        , "Guess {0} items correctly under one second");

        BuildGameProgressionLogicList(rep => rep.totalTimesYellowBananaWasSeen
        , RewardAndBarrier.TOTAL_TIMES_YELLOW_BANANA_WAS_SEEN_PROGRESSION
        , "See the yellow banana {0} times");

        BuildGameProgressionLogicList(rep => rep.totalTimesCocoWasPicked
        , RewardAndBarrier.TOTAL_TIMES_COCO_WAS_PICKED_PROGRESSION
        , "Guess Coco correctly {0} times");

        BuildGameProgressionLogicList(rep => rep.totalTimesChompWasPicked
        , RewardAndBarrier.TOTAL_TIMES_CHOMP_WAS_PICKED_PROGRESSION
        , "Guess Chomp correctly {0} times");

        BuildGameProgressionLogicList(rep => rep.totalCorrectGuessesUnderHalfASecond
        , RewardAndBarrier.TOTAL_CORRECT_GUESSES_UNDER_HALF_A_SECOND_PROGRESSION
        , "Guess {0} items correctly under half a second");

        BuildGameProgressionLogicList(rep => rep.totalGamesWithAndrewAndKelsey
        , RewardAndBarrier.TOTAL_GAMES_WITH_ANDREW_AND_KELSEY_PROGRESSION
        , "Play {0} games with Andrew and Kelsey");

        BuildGameProgressionLogicList(rep => rep.totalGamesWithCocoAndMuffin
        , RewardAndBarrier.TOTAL_GAMES_WITH_COCO_AND_MUFFIN_PROGRESSION
        , "Play {0} games with Coco and Muffin");

        BuildGameProgressionLogicList(rep => rep.totalGamesWithMonkeyAndPenguin
        , RewardAndBarrier.TOTAL_GAMES_WITH_MONKEY_AND_PENGUIN_PROGRESSION
        , "Play {0} games with Monkey and Penguin");

        BuildGameProgressionLogicList(rep => rep.totalCorrectGuessesStreak
        , RewardAndBarrier.TOTAL_CORRECT_GUESSES_STREAK_PROGRESSION
        , "Guess {0} items correctly in a row");
    }

    private void BuildGameProgressionLogicList(Func<GameProgressionRepresentation, float> getRepField, RewardAndBarrier[] rb, string reasonString)
    {
        gameProgressionLogicContainerList.Add(new GameProgressionLogicContainer(
            GameProgressionUtil.GetGameProgressionField(getRepField)
            , getRepField
            , rb
            , reasonString));
    }

    private Card generateCard()
    {
        Debug.Log("Generating card");       

        int cardIndex = UnityEngine.Random.Range(0, allCards.Count);
        Card card = allCards[cardIndex];

        return card;
    }
    private void NewRound()
    {
        Card card = generateCard();

        CardUtil.EntityEnum[] allEntities = new CardUtil.EntityEnum[CardUtil.Entities_2Card.Count];
        CardUtil.Entities_2Card.CopyTo(allEntities);

        CardUtil.EntityEnum[] currentEntities = card.cardEntities.Select(e => e.entity).ToArray();
        CardUtil.EntityEnum[] otherEntities = allEntities.Where(e => !currentEntities.Contains(e)).ToArray();


        useCorrectColor = false;
        HashSet<CardUtil.ColorEnum> colorsUsed = new HashSet<CardUtil.ColorEnum>();
        Dictionary<CardUtil.EntityEnum, CardUtil.ColorEnum> entityToColor = new Dictionary<CardUtil.EntityEnum, CardUtil.ColorEnum>();
        Array.ForEach(card.cardEntities, cardEntity =>
        {

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

            cardEntity.image.color = CardUtil.ColorColorMap[color];

        });

        if (currentEntities.Contains(CardUtil.EntityEnum.Banana) && entityToColor[CardUtil.EntityEnum.Banana] == CardUtil.ColorEnum.Yellow)
        {
            GameProgressionUtil.UpdateGameProgression(rep => rep.totalTimesYellowBananaWasSeen++);
        }

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

         cardGameObject = Instantiate(card, cardContainer.transform);

        //Enable all buttons

        SetButtonsEnabled(player1Buttons, true);        
        SetButtonsEnabled(player2Buttons, true);
        

        //Set up stats

        float time = Time.time;

        player1.Stats.AddPickedCard(time, card, entityToColor, useCorrectColor);
        player1.guessed = false;
        if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two)
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
            cpuCoroutines.Add(StartCoroutine(CpuGuess(cpu)));
        });


    }

    IEnumerator CpuGuess(Cpu cpu)
    {
        float delayModifier = 0;
        if (cpu.delayModiferDict != null)
        {
            cpu.delayModiferDict.TryGetValue(correctEntity.Value, out delayModifier);
        }

        float delayBeforeAnswer = RandomUtil.nonUniformDistributedRandom(cpu.delayLowerRangeBeforeAnswer + delayModifier, cpu.delayUpperRangeBeforeAnswer + delayModifier, x=> (float)(1f - Math.Pow(x - 1, 4)));

        yield return new WaitForSeconds(delayBeforeAnswer);

        CardUtil.EntityEnum inCorrectEntity = correctEntity.Value == CardUtil.EntityEnum.Banana ? CardUtil.EntityEnum.Coco : CardUtil.EntityEnum.Banana;

        float chanceOfCorrectModifier = 0;
        if (cpu.chanceOfCorrectModiferDict != null)
        {
            cpu.chanceOfCorrectModiferDict.TryGetValue(correctEntity.Value, out chanceOfCorrectModifier);
        }

        float randomPercent = RandomUtil.nonUniformDistributedRandom(0, 100, x => (float)Math.Pow(x - 1, 4));
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

    public void Guess(CardUtil.EntityEnum entity, Participant _participant = null)
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
            if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two && participant != player2 && !player2.guessed)
            {
                player2.Stats.AddMissed(correctEntity.Value);
            }
            GameUtil.cpuList.ForEach(cpu =>
            {
                if (participant != cpu && !cpu.guessed)
                {
                    cpu.Stats.AddMissed(correctEntity.Value);
                }
            });

            if (participant == player1 || (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two && participant == player2))
            {
                correctStreak++;
                if (useCorrectColor)
                {
                    GameProgressionUtil.UpdateGameProgression(rep => rep.totalCorrectCorrectlyColoredGuesses++);
                }
                else
                {
                    GameProgressionUtil.UpdateGameProgression(rep => rep.totalCorrectIncorrectlyColoredGuesses++);
                }
                if (correctEntity == CardUtil.EntityEnum.Coco)
                {
                    GameProgressionUtil.UpdateGameProgression(rep => rep.totalTimesCocoWasPicked++);
                }
                if (correctEntity == CardUtil.EntityEnum.Chomp)
                {
                    GameProgressionUtil.UpdateGameProgression(rep => rep.totalTimesChompWasPicked++);
                }
                if (timeToGuess < 1f)
                {
                    GameProgressionUtil.UpdateGameProgression(rep => rep.totalCorrectGuessesUnderOneSecond++);
                }
                if (timeToGuess < .5f)
                {
                    GameProgressionUtil.UpdateGameProgression(rep => rep.totalCorrectGuessesUnderHalfASecond++);
                }
                gameAudioManager.PlayCorrectGuess();
            }
            else
            {
                Cpu cpu = (Cpu) participant;
                gameAudioManager.PlayAVoice(cpu.voiceCorrect);
            }


        }
        else
        {
            if (SettingsUtil.IsPenaltiesAllowed())
            {
                participant.penalties++;
                if (participant.points - participant.penalties < 0 && !SettingsUtil.IsNegativeScoresAllowed())
                {
                    participant.penalties--;
                }
            }

            if (participant == player1 || (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two && participant == player2))
            {
                correctStreak = 0;
                gameAudioManager.PlayIncorrectGuess();
            } 
            else
            {
                Cpu cpu = (Cpu)participant;
                gameAudioManager.PlayAVoice(cpu.voiceIncorrect);

            }

        }


        if (participant == player1)
        {
            SetButtonsEnabled(player1Buttons, entity == correctEntity);
        }
        if (participant == player2)
        {
            SetButtonsEnabled(player2Buttons, entity == correctEntity);
        }

        participant.finalScore = participant.points - participant.penalties;
        UpdateScoresAndNameText();

        CheckForGameOver();
        bool player2Guessed = GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two && player2.guessed;
        if (entity == correctEntity || ((GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Single || player2Guessed) && player1.guessed && GameUtil.cpuList.All(cpu => cpu.guessed)))
        {

            if (cardGameObject != null)
            {
                DestroyImmediate(cardGameObject.gameObject);
                cardGameObject = null;
            }

            if (!gameOver)
                StartCoroutine(NewRoundWithDelay());
        }
    }

    private void SetButtonsEnabled(Button[] buttons, bool enabled)
    {

        Array.ForEach(buttons, btn =>
        {
            btn.interactable = enabled;
        });
    }

    IEnumerator NewRoundWithDelay()
    {
        cardInDelay = true;
        float totalDelay = SettingsUtil.GetCardDelay();

        roundSeperator.SetActive(totalDelay > MIN_ROUND_DELAY_TO_DISPLAY_ROUND_SEP);

        delayValue.text = "3";
        yield return new WaitForSeconds(totalDelay / 3);
        delayValue.text = "2";
        yield return new WaitForSeconds(totalDelay / 3);
        delayValue.text = "1";
        yield return new WaitForSeconds(totalDelay / 3);

        roundSeperator.SetActive(false);

        if (totalDelay > MIN_ROUND_DELAY_TO_DISPLAY_ROUND_SEP)
        {
            gameAudioManager.PlayCardFlip();
        }
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

        if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two)
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
            cpu1ScoreText.text = cpu1Score;
            cpu1NameText.text = GameUtil.cpuList.Count > 0 ? GameUtil.cpuList[0].name : "";
            cpu2ScoreText.text = cpu2Score;
            cpu2NameText.text = GameUtil.cpuList.Count > 1 ? GameUtil.cpuList[1].name : "";
            cpu3ScoreText.text = cpu3Score;
            cpu3NameText.text = GameUtil.cpuList.Count > 2 ? GameUtil.cpuList[2].name : "";
        }
    }

    private void CheckForGameOver()
    {
        if (GameSettingsUtil.GetGameMode() == GameSettingsUtil.GameModeEnum.Coco)
        {
            if (player1.finalScore >= GameUtil.pointsToReach
                || (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two && player2.finalScore >= GameUtil.pointsToReach)
                || GameUtil.cpuList.Any(cpu => cpu.finalScore >= GameUtil.pointsToReach))
                gameOver = true;
        }
        else if (GameSettingsUtil.GetGameMode() == GameSettingsUtil.GameModeEnum.GoGo && timer <= 0)
        {
            gameOver = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!cardInDelay && !gameOver)
        {
            if (GameSettingsUtil.GetGameMode() == GameSettingsUtil.GameModeEnum.Coco)
            {
                timer += Time.deltaTime;
            }
            else if (GameSettingsUtil.GetGameMode() == GameSettingsUtil.GameModeEnum.GoGo)
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
            if (cardGameObject != null)
            {
                DestroyImmediate(cardGameObject.gameObject);
                cardGameObject = null;
            }
            GameProgressionUtil.UpdateGameProgression(rep => rep.totalTimeSpentPlaying += GameSettingsUtil.GetGameMode() == GameSettingsUtil.GameModeEnum.GoGo ? GameUtil.timer : timer);
            if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Single)
            {
                GameProgressionUtil.UpdateGameProgression(rep => rep.totalSinglePlayerGamesPlayed++);
            }
            if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two)
            {
                GameProgressionUtil.UpdateGameProgression(rep => rep.totalTwoPlayerGamesPlayed++);
            }
            if (GameUtil.cpuList.All(cpu => cpu.finalScore < player1.finalScore) || (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two && GameUtil.cpuList.All(cpu => cpu.finalScore < player2.finalScore)))
            {
                GameProgressionUtil.UpdateGameProgression(rep => rep.totalCpusDefeated += GameUtil.cpuList.Count);
            }
            if (GameUtil.cpuList.Contains(Cpu.ANDREW) && GameUtil.cpuList.Contains(Cpu.KELSEY))
            {
                GameProgressionUtil.UpdateGameProgression(rep => rep.totalGamesWithAndrewAndKelsey++);
            }
            if (GameUtil.cpuList.Contains(Cpu.COCO) && GameUtil.cpuList.Contains(Cpu.MUFFIN))
            {
                GameProgressionUtil.UpdateGameProgression(rep => rep.totalGamesWithCocoAndMuffin++);
            }
            if (GameUtil.cpuList.Contains(Cpu.MONKEY) && GameUtil.cpuList.Contains(Cpu.PENGUIN))
            {
                GameProgressionUtil.UpdateGameProgression(rep => rep.totalGamesWithMonkeyAndPenguin++);
            }
            GameProgressionUtil.UpdateGameProgression(rep => rep.totalCorrectGuessesStreak += correctStreak);

            showingGameOverMenu = true;
            ShowGameOverMenu();
            
            gameProgressionLogicContainerList.ForEach(l =>
            {
                RewardAndBarrier[] list = GameProgressionUtil.GetCorrectRewards(l.rewardAndBarriers, l.initialValue, GameProgressionUtil.GetGameProgressionField(l.lambda));
                AttemptRewardUnlock(list, l.reasonString);
            });

            bananaRewardWindowManager.BeginUnlockPhase();

        }


    }

    private void AttemptRewardUnlock(RewardAndBarrier[] list, string reasonString)
    {

        Array.ForEach(list, rb =>
        {
            GameProgressionUtil.ChangeBananaCountBy(rb.Reward);
            bananaRewardWindowManager.AddRewardToUnlock(rb, reasonString);
        });
    }

    private void ShowGameOverMenu()
    {
        List<Statistics> statsList = new List<Statistics>();
        statsList.Add(player1.Stats);
        if (GameSettingsUtil.GetGameType() == GameSettingsUtil.GameTypeEnum.Two)
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
        int milliseconds = Mathf.FloorToInt(timer * 10) - Mathf.FloorToInt(timer) * 10;
        string niceTime = string.Format("{0:0}:{1:00}.{2:0}", minutes, seconds, milliseconds);

        return niceTime;
    }


}

