using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class JumbleTextBehaviour : MonoBehaviour
{
    private string textToJumble;
    private string jumbledText;
    private RectTransform rt;
    private Text t;
    // Start is called before the first frame update

    // void OnEnable()
    // {
    //     EventManager.OnDocument += fadeOut;
    // }

    // void OnDisable()
    // {
    //     EventManager.OnDocument -= fadeOut;
    // }
    void Start()
    {
        textToJumble = this.GetComponent<Text>().text;
        jumbledText = Shuffle(textToJumble);
        rt = GetComponent<RectTransform>();
        t = GetComponent<Text>();
    }


    public void fadeOut() {
        LeanTween.alphaText(rt, 0f, 0.6f).setEase(LeanTweenType.linear).setOnComplete(changeText);
    }

    public void fadeIn() {
        LeanTween.alphaText(rt, 1f, 0.6f).setEase(LeanTweenType.linear);
    }

    public void changeText() {
        t.text = jumbledText;
        fadeIn();
        // LeanTween.cancelAll();
    }

    string Shuffle(string str) {
        char[] array = str.ToCharArray();
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }

}
