using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;
using HTC.UnityPlugin.Vive;

public class TogglePointers : MonoBehaviour
{
    public GameObject reference;
    private bool pointersToggled = false;

    private void Update()
    {
        if (ViveInput.GetPressDown(HandRole.LeftHand, ControllerButton.DPadUp) || ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.DPadUp))
        {
            if (!pointersToggled)
            {
                reference.SetActive(true);
                pointersToggled = true;
            }
            else
            {
                reference.SetActive(false);
                pointersToggled = false;
            }
        }
    }

}
