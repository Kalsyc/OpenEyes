using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAwake : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider.value = PlayerPrefs.GetFloat("sfxVolume", 0.5f);
    }

}
