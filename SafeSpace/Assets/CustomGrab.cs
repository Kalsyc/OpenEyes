using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;
using UnityEngine.UI;
using Tobii.G2OM;

public class CustomGrab : MonoBehaviour, IGazeFocusable
{

    public GameObject objectReference;
    public GameObject controllerReference;
    public AudioSource clickAudio;
    public Material outlineMaterial;
    public float AnimationTime = 0.1f;

    private Transform objectTransform;
    private Transform controllerTransform;
    private bool inFocus = false;
    private bool isPlaying = false;
    private Vector3 originalPosition;
    private MeshRenderer meshRenderer;
    private Material originalMaterial;
    private Material[] normalMat = new Material[1];
    private Material[] highlightedMat = new Material[2];

    //Controller to set for trigger
    [Serializable]
    public class ControllerSelector
    {
        public HTC.UnityPlugin.Vive.HandRole controller;
    }

    private void Start()
    {
        meshRenderer = objectReference.GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.materials[0];
        normalMat[0] = originalMaterial;
        highlightedMat[0] = originalMaterial;
        highlightedMat[1] = outlineMaterial;
        meshRenderer.materials = normalMat;
        objectTransform = objectReference.GetComponent<Transform>();
        controllerTransform = controllerReference.GetComponent<Transform>();
        originalPosition = objectReference.GetComponent<Transform>().position;
        StartCoroutine(Pickup());

    }

    public ControllerSelector controllerToSet = new ControllerSelector();

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            inFocus = true;
            meshRenderer.materials = highlightedMat;
        }
        else
        {
            inFocus = false;
            meshRenderer.materials = normalMat;
        }
    }
    private void Update()
    {
    }

    IEnumerator Pickup()
    {
        while ((!inFocus || !ViveInput.GetPressDown(controllerToSet.controller, ControllerButton.Trigger)) && !isPlaying)
        {
            yield return null;
        }
        isPlaying = true;
        clickAudio.Play(0);
        objectTransform.position = controllerTransform.position;
        //Do something
        yield return new WaitForSeconds(5f);
        objectTransform.position = originalPosition;
        isPlaying = false;
        StartCoroutine(Pickup());
    }


}
