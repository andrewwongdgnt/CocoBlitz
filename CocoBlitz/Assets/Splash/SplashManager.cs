using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour {


    public Image splash; 
	// Use this for initialization
	void Start () {
        StartCoroutine(FinishSplash());
    }

    IEnumerator FinishSplash () {
        yield return new WaitForSeconds(4);

        float colorByte = 1;        
        while (colorByte > 0)
        {
            colorByte-=0.05f;
            splash.color = new Color(colorByte, colorByte, colorByte);
            yield return new WaitForSeconds(0.01f);

        }
        SceneManager.LoadScene("Main");

    }
}
