using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Handles the script for guided breathing mode
/// </summary>
public class GuidedBreathing : MonoBehaviour
{
    public int[] stringDuration;
    public string[] stringShown;
    public int glowingCircleStart;

    public int circleStart;
    public int textOff;
    public int textOn;

    public GameObject textObject;
    public GameObject breathingText;
    public GameObject glowingCircle;
    public GameObject breathingCircle;
    public GameObject setCircle;

    private bool isPlaying = false;

    private TMP_Text text;
    private Dictionary<int, Action> holder = new Dictionary<int, Action>();

    public void Start()
    {
        text = textObject.GetComponent<TMP_Text>();
        holder.Add(circleStart, PlaySetCircle);
        holder.Add(textOn, PlayBreathingText);
        holder.Add(textOff, StopBreathingText);
        holder.Add(glowingCircleStart, PlayGlowingCircle);
    }
    public void PlayScript()
    {
        if (isPlaying)
        {
            StopAllCoroutines();
            glowingCircle.SetActive(false);
            breathingCircle.SetActive(false);
            breathingText.SetActive(true);
            setCircle.SetActive(false);
        }
        StartCoroutine(PlayStep(stringShown));
    }

    IEnumerator PlayStep(string[] msg)
    {
        isPlaying = true;
        for (int i = 0; i < msg.Length; i++)
        {
            text.text = msg[i];
            if (holder.ContainsKey(i))
            {
                holder[i].Invoke();
            }
            yield return new WaitForSeconds(stringDuration[i]);
        }
    }

    private void PlaySetCircle()
    {
        setCircle.SetActive(true);
    }

    private void PlayBreathingText()
    {
        breathingText.SetActive(true);
    }

    private void StopBreathingText()
    {
        breathingText.SetActive(false);
    }    

    private void PlayBreathingCircle()
    {
        breathingCircle.SetActive(true);
    }    


    private void PlayGlowingCircle()
    {
        glowingCircle.SetActive(true);
    }

    private void StopGlowingCircle()
    {
        glowingCircle.SetActive(false);
    }

}
