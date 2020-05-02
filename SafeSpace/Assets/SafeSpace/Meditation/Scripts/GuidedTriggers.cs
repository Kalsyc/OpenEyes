using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedTriggers : MonoBehaviour
{
    public GameObject glowingCircle;

    public void PlayGlowingCircle()
    {
        glowingCircle.SetActive(true);
    }

    public void StopGlowingCircle()
    {
        glowingCircle.SetActive(false);
    }
}
