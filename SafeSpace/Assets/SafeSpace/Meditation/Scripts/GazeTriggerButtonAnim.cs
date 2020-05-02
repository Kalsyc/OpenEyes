using System;
using System.Collections;
using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class GazeTriggerButtonAnim : MonoBehaviour, IGazeFocusable
{
    public AudioSource clickAudio;
    public Button button;

    private bool inFocus = false;

    //Controller to set for trigger
    [Serializable]
    public class ControllerSelector
    {
        public HTC.UnityPlugin.Vive.HandRole controller;
    }
    public ControllerSelector controllerToSet = new ControllerSelector();

    private void Update()
    {
        if (ViveInput.GetPressDown(controllerToSet.controller, ControllerButton.Trigger) && inFocus)
        {
            Debug.Log("Trigger pressed with gaze");
            if (clickAudio != null)
            {
                clickAudio.Play(0);
            }
            Debug.Log("Button Pressed");
            StartCoroutine(PlayAnimation());
            button.onClick.Invoke();
            
        }
    }

    public IEnumerator PlayAnimation()
    {
        transform.Translate(new Vector3(0.0f, -0.1f));
        yield return new WaitForSeconds(1f);
        transform.Translate(new Vector3(0.0f, 0.1f));
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            inFocus = true;
        }
        else
        {
            inFocus = false;
        }
    }
}
