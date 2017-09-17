using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameModel : MonoBehaviour {

    public Card[] cards_2Entities;

	// Use this for initialization
	void Start () {
        Debug.Log("Game Begin ");
        Card card = cards_2Entities[UnityEngine.Random.Range(0, cards_2Entities.Length)];
        GameObject[] cardGameObjects = GameObject.FindGameObjectsWithTag("Card");
        Array.ForEach(cardGameObjects, ent =>
        {

            if (ent.GetComponent<Card>() != card)           
            {
                ent.SetActive(false);
            }
        });



        CardManager.EntityEnum[] allEntities = new CardManager.EntityEnum[CardManager.Entities_2Card.Count];
        CardManager.Entities_2Card.CopyTo(allEntities);

        CardManager.EntityEnum[] currentEntities = card.cardEntities.Select(e => e.entity).ToArray();
        CardManager.EntityEnum[] otherEntities = allEntities.Where(e => !currentEntities.Contains(e)).ToArray();

        bool useCorrectColor = false;
        HashSet<CardManager.ColorEnum> colorsUsed = new HashSet<CardManager.ColorEnum>();
        Dictionary<CardManager.EntityEnum, CardManager.ColorEnum> entityToColor = new Dictionary<CardManager.EntityEnum, CardManager.ColorEnum>();
        Array.ForEach(card.cardEntities, cardEntity => {

            int index = useCorrectColor ? UnityEngine.Random.Range(0, otherEntities.Length) : UnityEngine.Random.Range(0, otherEntities.Length+1);
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
                
            } while (colorsUsed.Contains(color)) ;
            colorsUsed.Add(color);
            entityToColor[cardEntity.entity] = color;
            Debug.Log(cardEntity.entity + ": " + color);
           
            cardEntity.spriteRenderer.color = CardManager.ColorColorMap[color];

        });

        CardManager.EntityEnum? correctEntity  = null;
        
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

        if (correctEntity== null)
            correctEntity = allEntities.Except(incorrectEntities).First();
        
        Debug.Log("correctEntity: " + correctEntity);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
