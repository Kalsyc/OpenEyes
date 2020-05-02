using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectLoader : MonoBehaviour
{
    public GameObject objectsToLoad;
    public GameObject toInstantiate;
    public GameObject breathingSet;
    public GameObject meditationSet;
    private bool noObjectsFound = false;

    public void Start()
    {
        SceneManager.sceneUnloaded += OnUnloadMenu;
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
        int mode;
        try
        {
            mode = PlayerPrefs.GetInt("mode");
        }
        catch
        {
            breathingSet.SetActive(true);
            meditationSet.SetActive(true);
            SceneManager.sceneUnloaded -= OnUnloadMenu;
            throw new PlayerPrefsException("No mode");
        }
        if (mode == 0)
        {
            breathingSet.SetActive(true);
        }
        else if (mode == 1)
        {
            meditationSet.SetActive(true);
        }
        SceneManager.sceneUnloaded -= OnUnloadMenu;
    }

}
