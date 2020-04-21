using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tobii;
public class DestroyLoadingScreen : MonoBehaviour
{
    public GameObject tobii;
    public GameObject sr;

    private bool isDestroyed = false;

    public void Start()
    {
        SceneManager.sceneUnloaded += OnUnloadLoading;
        //SceneManager.UnloadSceneAsync((int)SceneIndexes.LOADING_SCREEN);
    }

    public void Update()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            SceneManager.UnloadSceneAsync((int)SceneIndexes.LOADING_SCREEN);
        }
    }


    public void OnUnloadLoading(Scene current)
    {
        if (tobii != null && sr != null)
        {
            tobii.SetActive(true);
            sr.SetActive(true);
        }
        else
        {
            Debug.Log("Objects to load cannot be found... searching...");
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ToLoad"))
            {
                obj.SetActive(true);
            }
        }

        SceneManager.sceneUnloaded -= OnUnloadLoading;
    }

}
