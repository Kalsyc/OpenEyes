using System;
using Tobii.G2OM;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;
using UnityEngine.Events;

public class InteractableCharacter : MonoBehaviour, IGazeFocusable
{

    public GameObject cursor;
    public UnityEvent eventToInvoke;

    private bool inFocus = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inFocus && ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Trigger))
        {
            eventToInvoke.Invoke();
            cursor.SetActive(false);
            Destroy(this);
        }
    }
    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            cursor.SetActive(true);
            inFocus = true;
        }
        else
        {
            cursor.SetActive(false);
            inFocus = false;
        }
    }
}
