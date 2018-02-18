using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UCW : MonoBehaviour {

    public UCWManager manager;
    public Image characterImage;
    public Text characterNameText;
    public Animator anim;

    // Use this for initialization
    void Start () {
    }

    public void Unlock()
    {
        manager.Unlock();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        anim.SetTrigger("Show");
    }

    public void Hide()
    {

        gameObject.SetActive(false);
        manager.Hide();
    }

    public void SetCharacterSprite(Sprite sprite)
    {
        characterImage.sprite = sprite;
    }

    public void SetCharacterName(string name)
    {
        characterNameText.text = name;
    }
    

}
