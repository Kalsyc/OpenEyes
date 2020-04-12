using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectLoader : MonoBehaviour
{
    public GameObject objectsToLoad;

    public void Start()
    {
        SceneManager.sceneUnloaded += OnUnloadMenu;
    }
    public void OnUnloadMenu(Scene current)
    {
        objectsToLoad.SetActive(true);
        SceneManager.sceneUnloaded -= OnUnloadMenu;
    }

}
