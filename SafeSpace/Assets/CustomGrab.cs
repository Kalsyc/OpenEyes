using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using UnityEngine.UI;
using Tobii.G2OM;
using UnityEngine.Events;

public class CustomGrab : MonoBehaviour, IGazeFocusable
{

    public GameObject objectReference;
    public GameObject controllerReference;
    public AudioSource clickAudio;
    public Material outlineMaterial;
    public float AnimationTime = 0.1f;
    public List<UnityEvent> eventList;
    public float grabDuration;
    public GameObject cursor;
    public bool isHighlightable;

    private Transform objectTransform;
    private Transform controllerTransform;
    private bool inFocus = false;
    private bool isPlaying = false;
    private Vector3 originalPosition;
    private MeshRenderer meshRenderer;
    private Material[] originalMaterial;
    private int numOfMaterial;
    private Material[] highlightedMat;
    private int numOfEvents;
    private bool[] boolPlay;

    //Controller to set for trigger
    [Serializable]
    public class ControllerSelector
    {
        public HTC.UnityPlugin.Vive.HandRole controller;
    }

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
            meshRenderer.materials = originalMaterial;
        }
        objectTransform = objectReference.GetComponent<Transform>();
        controllerTransform = controllerReference.GetComponent<Transform>();
        originalPosition = objectReference.GetComponent<Transform>().position;

        StartCoroutine(Pickup());

    }

    public ControllerSelector controllerToSet = new ControllerSelector();

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
    private void Update()
    {
    }

    IEnumerator Pickup()
    {
        while (!(inFocus && !isPlaying && ViveInput.GetPressDown(controllerToSet.controller, ControllerButton.Trigger)))
        //while ((!inFocus || !ViveInput.GetPressDown(controllerToSet.controller, ControllerButton.Trigger)) && !isPlaying)
        {
            yield return null;
        }
        cursor.SetActive(false);
        isPlaying = true;
        clickAudio.Play(0);
        objectTransform.position = controllerTransform.position;
        foreach (UnityEvent eventInstance in eventList)
        {
            eventInstance.Invoke();
        }
        yield return new WaitForSeconds(grabDuration);
        objectTransform.position = originalPosition;
        isPlaying = false;
        StartCoroutine(Pickup());
    }


}
