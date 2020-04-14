using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectLoader : MonoBehaviour
{
    public GameObject objectsToLoad;
    public GameObject toInstantiate;

    public void Start()
    {
        SceneManager.sceneUnloaded += OnUnloadMenu;
    }
    public void OnUnloadMenu(Scene current)
    {
        if (objectsToLoad != null)
        {
            objectsToLoad.SetActive(true);
        }
        else
        {
            Debug.Log("Objects to load cannot be found... searching...");
            GameObject.FindGameObjectWithTag("ToLoad").SetActive(true);
            
        }
        toInstantiate.SetActive(true);
        SceneManager.sceneUnloaded -= OnUnloadMenu;
    }

}
