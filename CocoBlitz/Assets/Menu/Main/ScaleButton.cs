using UnityEngine;
using System;
using UnityEngine.UI;

public class ScaleButton : MonoBehaviour {

    public GameObject[] childrenToScale;
    public float scaleTo;
	
    public void ScaleChildren(bool shrink)
    {
        Button btn = GetComponent<Button>();
        if (btn.IsInteractable())
        {
            Array.ForEach(childrenToScale, go =>
            {
                go.transform.localScale = shrink ? new Vector3(scaleTo, scaleTo, scaleTo) : new Vector3(1, 1, 1);
            });
        }
    }
}
