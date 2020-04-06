using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class OnGazeShowText : MonoBehaviour, IGazeFocusable
{
    public GameObject textToShow;

    private bool isActive = false;

    //The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus
    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus)
        {
            isActive = true;
            textToShow.SetActive(true);
        }

        else
        {
            isActive = false;
            StartCoroutine(WaitOffRoutine());
        }
    }

    private IEnumerator WaitOffRoutine()
    {
        yield return new WaitForSeconds(10);
        if (!isActive)
        {
            textToShow.SetActive(false);
        }
    }
}
