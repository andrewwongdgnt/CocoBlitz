using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameModel : MonoBehaviour {

    public Card[] cards_2Entities;
    public Text pointsScoreText;
    public Text penaltiesScoreText;
    public Text timerText;

    private int points_score;
    private int penalties_score;
    private CardManager.EntityEnum? correctEntity = null;
    private GameObject[] cardGameObjects;

    private float timer=0f;
    private int pointsToReach = 0;

    private bool gameOver;

    // Use this for initialization
    void Start () {
        cardGameObjects = GameObject.FindGameObjectsWithTag("Card");
        timer = GameManager.currentGameMode == GameManager.GameModeEnum.RackUpThePoints ? GameManager.timer : 0f;
        pointsToReach = 0;
        gameOver = false;
        Begin();
    }

    public void Begin()
    {
        Debug.Log("Game Begin ");
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

        
    }

    public void Guess(CardManager.EntityEnum entity)
    {
        if (gameOver)
            SceneManager.LoadScene("Game");
        Debug.Log("your guess: " + entity);
        Debug.Log("correct entity: " + correctEntity);

        if (entity == correctEntity)
        {
            points_score++;
            pointsScoreText.text = points_score.ToString();
        }
        else
        {
            penalties_score++;
            penaltiesScoreText.text = penalties_score.ToString();
        }
        pointsToReach = points_score - penalties_score;
        CheckForGameOver();
        if (!gameOver)
            Begin();
    }

    private void CheckForGameOver()
    {
        if (GameManager.currentGameMode == GameManager.GameModeEnum.FastestTime)
        {
            if (pointsToReach >= GameManager.pointsToReach)
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

        if (GameManager.currentGameMode == GameManager.GameModeEnum.FastestTime)
        {
            timer += Time.deltaTime;
        }
        else if (GameManager.currentGameMode == GameManager.GameModeEnum.RackUpThePoints)
        {
            timer -= Time.deltaTime;
        }

        CheckForGameOver();
        if (!gameOver) { 
            timerText.text = TransformTime(timer);
            timerText.color = new Color32(0, 0, 0, 255);
        }
        else {
            timerText.color = new Color32(0, 255, 0, 255);
        }
    }

    private string TransformTime(float timer)
    {
        if (timer < 0)
            return TransformTime(0);
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        return niceTime;
    }
}
