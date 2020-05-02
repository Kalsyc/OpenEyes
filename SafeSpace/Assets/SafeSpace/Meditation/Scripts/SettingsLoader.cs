using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsLoader : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject pausePanel;
    public GameObject medSliderObject;
    public GameObject sfxSliderObject;

    private Slider medSlider;
    private Slider sfxSlider;

    public void Start()
    {
        medSlider = medSliderObject.GetComponent<Slider>();
        sfxSlider = sfxSliderObject.GetComponent<Slider>();
    }

    public void LoadPause()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void LowerMedVolume()
    {
        float currentValue = medSlider.value;
        medSlider.value = Mathf.Max(0f, medSlider.value - 0.1f);
    }

    public void LowerSFXVolume()
    {
        float currentValue = sfxSlider.value;
        sfxSlider.value = Mathf.Max(0f, sfxSlider.value - 0.1f);
    }

    public void IncreaseMedVolume()
    {
        float currentValue = medSlider.value;
        medSlider.value = Mathf.Min(1f, medSlider.value + 0.1f);
    }
    public void IncreaseSFXVolume()
    {
        float currentValue = sfxSlider.value;
        sfxSlider.value = Mathf.Min(1f, sfxSlider.value + 0.1f);
    }

}
