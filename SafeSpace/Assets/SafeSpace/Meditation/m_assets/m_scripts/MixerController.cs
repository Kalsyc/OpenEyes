using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioClip[] songs;


    // reference to sliders to update them automatically
    [Space(10)]
    public Slider meditateSlider;
    public Slider sfxSlider;

    public void SetMeditateVolume(float volume)
    {
        audioMixer.SetFloat("meditateVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void Start()
    {
        meditateSlider.value = PlayerPrefs.GetFloat("meditateVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
    }

    private void onDisable()
    {
        float meditateVolume = 0;
        float sfxVolume = 0;

        audioMixer.GetFloat("meditateVolume", out meditateVolume);
        audioMixer.GetFloat("sfxVolume", out sfxVolume);

        PlayerPrefs.SetFloat("meditateVolume", meditateVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }

}
