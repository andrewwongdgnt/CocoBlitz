using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaRewardWindow : MonoBehaviour {

    public BananaRewardWindowManager manager;
    public Text reasonText;
    public Text rewardText;
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
    

    public void SetReason(string reason)
    {
        reasonText.text = reason;
    }

    public void SetReward(string reward)
    {
        rewardText.text = reward;
    }


}
