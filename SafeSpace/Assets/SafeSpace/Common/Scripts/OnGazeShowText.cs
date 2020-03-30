using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class OnGazeShowText : MonoBehaviour, IGazeFocusable
{
    public GameObject textToShow;

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
            textToShow.SetActive(true);
        }
        //If this object lost focus, fade the object's color to it's original color
        else
        {
            textToShow.SetActive(false);
        }
    }
}
