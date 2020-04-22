using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehaviour : MonoBehaviour
{
    public AudioSource innerVoiceOne;
    public AudioSource innerVoiceTwo;
    public AudioSource innerVoiceThree;
    public AudioSource innerVoiceFour;
    public AudioSource innerVoiceFive;
    // public string eventType;
    // Start is called before the first frame update

    private void OnEnable() {
        innerVoiceOne.Stop();
        innerVoiceTwo.Stop();
        innerVoiceThree.Stop();
        innerVoiceFour.Stop();
        innerVoiceFive.Stop();
    }
    public void playVoiceOne()
    {
        innerVoiceTwo.Stop();
        innerVoiceThree.Stop();
        innerVoiceFour.Stop();
        innerVoiceFive.Stop();
        innerVoiceOne.Play();
        Debug.Log("Playing voice 1");
    }

    public void playVoiceTwo()
    {
        innerVoiceOne.Stop();
        innerVoiceThree.Stop();
        innerVoiceFour.Stop();
        innerVoiceFive.Stop();
        innerVoiceTwo.Play();
        Debug.Log("Playing voice 2");
    }

    public void playVoiceThree()
    {
        innerVoiceOne.Stop();
        innerVoiceTwo.Stop();
        innerVoiceFour.Stop();
        innerVoiceFive.Stop();
        innerVoiceThree.Play();
        Debug.Log("Playing voice 3");
    }

    public void playVoiceFour()
    {
        innerVoiceOne.Stop();        
        innerVoiceTwo.Stop();
        innerVoiceThree.Stop();
        innerVoiceFive.Stop();
        innerVoiceFour.Play();
        Debug.Log("Playing voice 4");
    }

    public void playVoiceFive()
    {
        innerVoiceOne.Stop();
        innerVoiceTwo.Stop();
        innerVoiceThree.Stop();
        innerVoiceFour.Stop();
        innerVoiceFive.Play();
        Debug.Log("Playing voice 5");
    }

}
