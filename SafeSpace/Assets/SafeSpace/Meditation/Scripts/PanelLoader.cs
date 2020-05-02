using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void checkModes()
    {
        if(PlayerPrefs.GetInt("mode") == 0)
        {
            Debug.Log("Mode is 0 here.");
        } else
        {
            Debug.Log("Mode is 1 here.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
