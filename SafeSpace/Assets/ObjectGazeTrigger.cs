using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using UnityEngine.UI;
using Tobii.G2OM;
using UnityEngine.Events;

public class ObjectGazeTrigger : MonoBehaviour, IGazeFocusable
{

    public GameObject objectReference;
    public AudioSource clickAudio;
    public Material outlineMaterial;
    public List<UnityEvent> eventList;
    public float eventDelay;
    public float concurrentDelay;
    public GameObject cursor;
    public bool toRepeat = false;

    private bool inFocus = false;
    private bool isPlaying = false;
    private MeshRenderer meshRenderer;
    private Material[] originalMaterial;
    private int numOfMaterial;
    private Material[] highlightedMat;

    //Controller to set for trigger
    [Serializable]
    public class ControllerSelector
    {
        public HTC.UnityPlugin.Vive.HandRole controller;
    }

    private void Start()
    {
        meshRenderer = objectReference.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.materials;
        numOfMaterial = originalMaterial.Length;
        highlightedMat = new Material[numOfMaterial + 1];
        originalMaterial.CopyTo(highlightedMat, 0);
        highlightedMat[numOfMaterial] = outlineMaterial;
        StartCoroutine(Pickup());

    }

    public ControllerSelector controllerToSet = new ControllerSelector();

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus && !isPlaying)
        {
            cursor.SetActive(true);
            inFocus = true;
            meshRenderer.materials = highlightedMat;
        }
        else
        {
            cursor.SetActive(false);
            inFocus = false;
            meshRenderer.materials = originalMaterial;
        }
    }
    private void Update()
    {
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
        foreach (UnityEvent eventInstance in eventList)
        {
            eventInstance.Invoke();
            yield return new WaitForSeconds(eventDelay);
        }
        yield return new WaitForSeconds(concurrentDelay);
        if (toRepeat)
        {
            isPlaying = false;
            StartCoroutine(Pickup());
        }

    }


}
