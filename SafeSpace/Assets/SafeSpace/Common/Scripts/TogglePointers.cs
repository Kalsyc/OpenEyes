using UnityEngine;
using HTC.UnityPlugin.Vive;

/// <summary>
/// Pointers that toggle on or off to help with interacting with buttons.
/// </summary>
public class TogglePointers : MonoBehaviour
{
    public GameObject reference;
    private bool pointersToggled = false;

    private void Update()
    {
        // toggle when player presses directional pad up on either left or right controller
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
