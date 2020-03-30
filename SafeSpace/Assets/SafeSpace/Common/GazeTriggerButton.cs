// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved - Modified by Kalsyc
using System;
using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

//Monobehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object receives focus
public class GazeTriggerButton : MonoBehaviour, IGazeFocusable
{
    //Reference to button being invoked
    public GameObject buttonReference;

    //default color
    public Color HighlightColor = Color.red;

    //animation time for fade
    public float AnimationTime = 0.1f;

    //Controller to set for trigger
    [Serializable]
    public class ControllerSelector
    {
        public HTC.UnityPlugin.Vive.HandRole controller;
    }

    public ControllerSelector controllerToSet = new ControllerSelector();

    private Graphic _graphic;
    private Color _originalColor;
    private Color _targetColor;
    private bool inFocus;

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
            Debug.Log("Gaze focused");
            _targetColor = HighlightColor;
            inFocus = true;
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
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
    }

    private void Update()
    {
        //This lerp will fade the color of the object
        _graphic.color = Color.Lerp(_graphic.color, _targetColor, Time.deltaTime * (1 / AnimationTime));

        //check if user has focused on button and pressed trigger
        if (ViveInput.GetPressDown(controllerToSet.controller, ControllerButton.Trigger) && inFocus)
        {
            Debug.Log("Trigger pressed with gaze");
            buttonReference.GetComponent<Button>().onClick.Invoke();
        }
    }
}   
