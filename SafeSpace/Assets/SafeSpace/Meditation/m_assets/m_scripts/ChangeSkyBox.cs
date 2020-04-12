using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkyBox : MonoBehaviour
{
    public Material skyboxOne;
    public Material skyboxTwo;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = skyboxOne;
    }
}
