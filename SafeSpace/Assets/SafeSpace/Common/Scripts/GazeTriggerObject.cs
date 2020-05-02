using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using UnityEngine.UI;
using Tobii.G2OM;
using UnityEngine.Events;

/// <summary>
/// Enables object to be highlighted with a cursor/material to signify that it is interactable and handles the interaction should the user gaze on the object
/// </summary>
public class GazeTriggerObject : MonoBehaviour, IGazeFocusable
{

    public GameObject objectReference;
    public GameObject cursor;

    //audio to be played when interacted
    public AudioSource clickAudio;

    //material used to outline interactable object
    public Material outlineMaterial;

    //events to be invoked
    public List<UnityEvent> eventList;

    //delays
    public float eventDelay;
    public float concurrentDelay;

    public bool toRepeat = false;
    public bool isHighlightable;

    public int limit;

    private bool inFocus = false;
    private bool isPlaying = false;

    private int numOfMaterial;
    private int count = 0;

    private MeshRenderer meshRenderer;
    private Material[] originalMaterial;
    private Material[] highlightedMat;

    //Controller to set for trigger
    [Serializable]
    public class ControllerSelector
    {
        public HTC.UnityPlugin.Vive.HandRole controller;
    }


    public ControllerSelector controllerToSet = new ControllerSelector();


    private void Start()
    {
        if (isHighlightable)
        {
            meshRenderer = objectReference.GetComponent<MeshRenderer>();
            originalMaterial = meshRenderer.materials;
            numOfMaterial = originalMaterial.Length;
            highlightedMat = new Material[numOfMaterial + 1];
            originalMaterial.CopyTo(highlightedMat, 0);
            highlightedMat[numOfMaterial] = outlineMaterial;
        }

        StartCoroutine(Pickup());

    }

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus && !isPlaying)
        {
            cursor.SetActive(true);
            inFocus = true;
            if (isHighlightable)
            {
                meshRenderer.materials = highlightedMat;
            }

        }
        else
        {
            cursor.SetActive(false);
            inFocus = false;
            if (isHighlightable)
            {
                meshRenderer.materials = originalMaterial;
            }

        }
    }

    IEnumerator Pickup()
    {
        while (!(inFocus && !isPlaying && ViveInput.GetPressDown(controllerToSet.controller, ControllerButton.Trigger)))
        {
            yield return null;
        }

        isPlaying = true;
        cursor.SetActive(false);
        clickAudio.Play(0);
        UnityEvent events = eventList[count];
        events.Invoke();
        count++;
        yield return new WaitForSeconds(eventDelay);

        //if there are multiple events, repeat
        if (toRepeat || count < limit)
        {
            isPlaying = false;
            StartCoroutine(Pickup());
        }

    }


}
