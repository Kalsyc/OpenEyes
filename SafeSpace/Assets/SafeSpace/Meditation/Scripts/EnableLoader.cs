using UnityEngine;

/// <summary>
/// Loads the objects. This is done because of a glitch or bug with Unity whereby GameObjects sometimes don't load properly when unloading async with TobiiXR and SRWorks
/// </summary>
public class EnableLoader : MonoBehaviour
{
    private bool foundObject = false;
    GameObject obj;

    private void Update()
    {
        //Debug.Log("Finding...");
        if (foundObject)
        {
            return;
        }
        obj = GameObject.FindGameObjectWithTag("OnEnable");
        if (obj != null)
        {
            obj.SetActive(true);
            foundObject = true;
        }
    }
}
