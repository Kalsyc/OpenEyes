using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeLoader : MonoBehaviour
{
    private int menuEnvIndex = (int)SceneIndexes.MEDITATION_MENU;

    public void UnloadCurrentScene()
    {
        GameObject.FindGameObjectWithTag("OnEnable").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("OnEnable").transform.GetChild(0).gameObject.GetComponent<ObjectLoader>().enabled = true;
        SceneManager.UnloadSceneAsync(menuEnvIndex);

    }
}
