using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelLoader : MonoBehaviour
{
    private GameObject pointer;
    private int toLoad;

    void Start()
    {
        while (pointer == null)
        {
            pointer = GameObject.FindGameObjectWithTag("NextLevelPointer");
        }
        Debug.Log(pointer);
        toLoad = (int)pointer.GetComponent<NextLevelPointer>().GetPointer();


    }

    public void LoadLevel()
    {
        Destroy(pointer);
        SceneManager.LoadScene(toLoad);
    }

}
