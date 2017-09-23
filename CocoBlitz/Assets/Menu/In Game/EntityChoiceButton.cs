using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChoiceButton : MonoBehaviour {

    public CardManager.EntityEnum entity;

    public GameModel gameModel;

	public void Guess()
    {
        gameModel.Guess(entity);
    }
}
