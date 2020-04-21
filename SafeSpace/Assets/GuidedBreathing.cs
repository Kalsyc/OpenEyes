using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class GuidedBreathing : MonoBehaviour
{
    public int[] stringDuration;
    public string[] stringShown;
    // PlayGlowingCircle -> StopGlowingCircle -> Play CircleAnim
    public int glowingCircleStart;
    public int glowingCircleEnd;
    public int breathingCircleStart;

    public GameObject textObject;
    public GameObject glowingCircle;
    public GameObject breathingCircle;

    private bool isPlaying = false;

    private TMP_Text text;
    private Dictionary<int, Action> holder = new Dictionary<int, Action>();

    public void Start()
    {
        text = textObject.GetComponent<TMP_Text>();
        holder.Add(glowingCircleStart, PlayGlowingCircle);
        holder.Add(glowingCircleEnd, StopGlowingCircle);
        holder.Add(breathingCircleStart, PlayBreathingCircle);
    }
    public void PlayScript()
    {
        if (isPlaying)
        {
            StopAllCoroutines();
            glowingCircle.SetActive(false);
            breathingCircle.SetActive(false);
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
                Debug.Log("Helpo");
                holder[i].Invoke();
            }
            yield return new WaitForSeconds(stringDuration[i]);
        }
        isPlaying = false;
    }

    public void PlayBreathingCircle()
    {
        breathingCircle.SetActive(true);
    }    


    public void PlayGlowingCircle()
    {
        glowingCircle.SetActive(true);
    }

    public void StopGlowingCircle()
    {
        glowingCircle.SetActive(false);
    }

}
