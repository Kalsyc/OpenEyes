// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved - Modified by Kalsyc
using System;
using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Monobehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object 
/// receives focus and invokes events when the user interacts with the button.
/// </summary>
public class GazeTriggerButton : MonoBehaviour, IGazeFocusable
{
    //Reference to button being invoked
    public Button buttonReference;

    //default color
    public Color highlightColor = Color.red;

    //default text color
    public Color textColor = Color.red;

    //animation time for fade
    public float AnimationTime = 0.1f;

    //select text if there is to highlight
    public GameObject textObject;

    //select audio
    public AudioSource selectAudio;

    //click audio
    public AudioSource clickAudio;

    //Controller to set for trigger
    [Serializable]
    public class ControllerSelector
    {
        public HTC.UnityPlugin.Vive.HandRole controller;
    }

    public ControllerSelector controllerToSet = new ControllerSelector();

    private Graphic _graphic;
    private Text text;
    private Color _originalColor;
    private Color _targetColor;
    private Color targetTextColor;
    private Color originalTextColor;
    private bool inFocus;
    private bool containsText = false;

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
            Debug.Log("Gaze focused");
            _targetColor = highlightColor;
            inFocus = true;
            if (containsText)
            {
                targetTextColor = textColor;
            }
            if (clickAudio != null)
            {
                selectAudio.Play(0);
            }

        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
            if (containsText)
            {
                targetTextColor = originalTextColor;
            }
            inFocus = false;
            _targetColor = _originalColor;
        }
    }

    private void Start()
    {
        inFocus = false;
        _graphic = GetComponent<Graphic>();
        _originalColor = _graphic.color;
        _targetColor = _originalColor;
        if (textObject != null)
        {
            text = textObject.GetComponent<Text>();
            Debug.Log(text);
            originalTextColor = text.color;
            targetTextColor = originalTextColor;
            containsText = true;
        }

    }

    private void Update()
    {
        //This lerp will fade the color of the object
        _graphic.color = Color.Lerp(_graphic.color, _targetColor, Time.deltaTime * (1 / AnimationTime));
        if (containsText)
        {
            text.color = Color.Lerp(text.color, targetTextColor, Time.deltaTime * (1 / AnimationTime));
        }
        //check if user has focused on button and pressed trigger
        if (ViveInput.GetPressDown(controllerToSet.controller, ControllerButton.Trigger) && inFocus)
        {
            Debug.Log("Trigger pressed with gaze");
            if (clickAudio != null)
            {
                clickAudio.Play(0);
            }
                
            buttonReference.onClick.Invoke();
        }
    }
}   
