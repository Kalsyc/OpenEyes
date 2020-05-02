using UnityEngine;

/// <summary>
/// Renders skybox in scene.
/// </summary>
public class ChangeSkyBox : MonoBehaviour
{
    public Material skyboxOne;

    void Start()
    {
        RenderSettings.skybox = skyboxOne;
    }
}
