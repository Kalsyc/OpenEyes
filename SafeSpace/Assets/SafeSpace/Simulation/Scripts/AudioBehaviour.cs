using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
    public AudioSource innerVoiceOne;
    public AudioSource innerVoiceTwo;
    // public string eventType;
    // Start is called before the first frame update

    private void OnEnable() {
        innerVoiceOne.Stop();
        innerVoiceTwo.Stop();
    }
    public void playVoiceOne()
    {
        innerVoiceTwo.Stop();
        innerVoiceOne.Play();
        Debug.Log("Playing voice 1");
    }

    public void playVoiceTwo()
    {
        innerVoiceOne.Stop();
        innerVoiceTwo.Play();
        Debug.Log("Playing voice 2");
    }

}
