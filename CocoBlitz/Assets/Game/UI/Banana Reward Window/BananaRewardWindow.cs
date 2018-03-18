using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaRewardWindow : MonoBehaviour {

    public GameAudioManager gameAudioManager;
    public BananaRewardWindowManager manager;
    public Text reasonText;
    public Text rewardText;
    public Text okToAllText;
    public Animator anim;

    private int countLeft;

    // Use this for initialization
    void Start () {
    }

    public void Unlock()
    {
        gameAudioManager.PlayMainButtonClick();
        countLeft--;
        UpdateOkToAllBtnText();
        manager.Unlock();
    }
    public void UnlockAll()
    {
        gameAudioManager.PlayMainButtonClick();
        manager.UnlockAll();
    }

    public void Show(int countLeft)
    {
        this.countLeft = countLeft;
        UpdateOkToAllBtnText();
        gameObject.SetActive(true);
        anim.SetTrigger("Show");
    }

    private void UpdateOkToAllBtnText()
    {

        okToAllText.text = string.Format("Ok to all ({0})", countLeft);
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
