using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class WakeBehaviour : MonoBehaviour
{
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    public void FadeOut() {
        LeanTween.alpha(rt, 0f, 2f).setEase(LeanTweenType.linear);
    }
}
