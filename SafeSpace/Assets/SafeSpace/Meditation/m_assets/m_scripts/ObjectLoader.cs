using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectLoader : MonoBehaviour
{
    public GameObject objectsToLoad;
    public GameObject toInstantiate;
    private bool noObjectsFound = false;

    public void Start()
    {
        SceneManager.sceneUnloaded += OnUnloadMenu;
    }

    public void Update()
    {
        if (noObjectsFound)
        {
            objectsToLoad = GameObject.FindGameObjectWithTag("ToLoad");
            if (objectsToLoad != null)
            {
                objectsToLoad.SetActive(true);
                noObjectsFound = false;
            }
        }
    }
    public void OnUnloadMenu(Scene scene)
    {
        if (objectsToLoad != null)
        {
            objectsToLoad.SetActive(true);
        }
        else
        {
            Debug.Log("Objects to load cannot be found... searching...");
            objectsToLoad = GameObject.FindGameObjectWithTag("ToLoad");
            if (objectsToLoad != null)
            {
                objectsToLoad.SetActive(true);
            }
            else
            {
                noObjectsFound = true;
            }
        }
        toInstantiate.SetActive(true);
        SceneManager.sceneUnloaded -= OnUnloadMenu;
    }

}
