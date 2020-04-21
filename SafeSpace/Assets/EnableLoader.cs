using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLoader : MonoBehaviour
{
    private bool foundObject = false;
    GameObject obj;

    private void Update()
    {
        Debug.Log("Finding...");
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
