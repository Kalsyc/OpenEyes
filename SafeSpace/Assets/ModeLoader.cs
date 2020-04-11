using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeLoader : MonoBehaviour
{
    private int menuEnvIndex = (int)SceneIndexes.MEDITATION_MENU;

    public void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(menuEnvIndex);
    }
}
