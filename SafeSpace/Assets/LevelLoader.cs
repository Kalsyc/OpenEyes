using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    private GameObject pointer;
    private int toLoad;
    private AsyncOperation operation;
    private float progress;

    public Slider slider;
    public GameObject button;
    void Start()
    {
        while (pointer == null)
        {
            pointer = GameObject.FindGameObjectWithTag("NextLevelPointer");
        }
        Debug.Log(pointer);
        toLoad = (int)pointer.GetComponent<NextLevelPointer>().GetPointer();
        StartCoroutine(LoadAsync(toLoad));


    }

    void Update()
    {
        if (progress == 1f)
        {


            button.SetActive(true);
        }
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Debug.Log(progress);
            yield return null;
        }

    }

    public void EndLoadingScreen()
    {
        operation.allowSceneActivation = true;

    }

    IEnumerator SkipFrame(int i)
    {
        for (int j = 0; j < i; j++)
        {
            yield return 0;
        }
    }

}
