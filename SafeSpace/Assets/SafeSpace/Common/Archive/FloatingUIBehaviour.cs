using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class FloatingUIBehaviour : MonoBehaviour
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
    }

    public void FadeIn() {
        LeanTween.alpha(panelRect, 1f, 1f).setEase(LeanTweenType.linear);
    }
    public void FadeOut() {
        LeanTween.alpha(panelRect, 0f, 1.5f).setEase(LeanTweenType.linear);
    }

    // public void MoveUp() { // not working
    //     LeanTween.moveY(gameObject, 0.0001f, 2.0f).setEase(LeanTweenType.easeOutQuad);
    // }
}
