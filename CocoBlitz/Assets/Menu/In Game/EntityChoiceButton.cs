using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityChoiceButton : MonoBehaviour {

    public CardUtil.EntityEnum entity;

    public GameModel gameModel;

	public void Guess()
    {
        gameModel.Guess(entity);
    }
    public void GuessForPlayer2()
    {
        gameModel.GuessForPlayer2(entity);
    }
}
