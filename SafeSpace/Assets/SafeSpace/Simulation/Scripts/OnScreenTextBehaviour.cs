using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class OnScreenTextBehaviour : MonoBehaviour
{
    private RectTransform panelRect;
    private Text onScreentext;
    // Start is called before the first frame update
    void Start()
    {
        onScreentext = GetComponentInChildren<Text>();
        panelRect = GetComponent<RectTransform>();
        LeanTween.alpha(panelRect, 0f, 0f).setEase(LeanTweenType.linear);
    }

    public void ShowText(string text) {
        FadeIn();
        onScreentext.text = text;
        Invoke("FadeOut", 5.0f);
    }

    public void FadeIn() {
        LeanTween.alpha(panelRect, 1f, 2f).setEase(LeanTweenType.linear);
    }

    public void FadeOut() {
        LeanTween.alpha(panelRect, 0f, 2f).setEase(LeanTweenType.linear);
    }
}
