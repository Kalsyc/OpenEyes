using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentLoader : MonoBehaviour
{
    private int currentLoadedEnvIndex = -1;
    private int flowerEnvIndex = (int)SceneIndexes.MEDITATION_FLOWERS;
    private int grassEnvIndex = (int)SceneIndexes.MEDITATION_GRASS;

    public GameObject unselectedWarning;
    public GameObject envPanel;

    public void LoadFlowerEnv()
    {
        unselectedWarning.SetActive(false);

        if (currentLoadedEnvIndex != flowerEnvIndex)
        {
            if (currentLoadedEnvIndex != -1)
            {
                SceneManager.UnloadSceneAsync(currentLoadedEnvIndex);
            }
            SceneManager.LoadSceneAsync(flowerEnvIndex, LoadSceneMode.Additive);
            currentLoadedEnvIndex = flowerEnvIndex;
        }
    }

    public void LoadGrassEnv()
    {
        unselectedWarning.SetActive(false);

        if (currentLoadedEnvIndex != grassEnvIndex)
        {
            if (currentLoadedEnvIndex != -1)
            {
                SceneManager.UnloadSceneAsync(currentLoadedEnvIndex);
            }
            SceneManager.LoadSceneAsync(grassEnvIndex, LoadSceneMode.Additive);
            currentLoadedEnvIndex = grassEnvIndex;
        }
    }

    public void CheckSelected()
    {
        if (currentLoadedEnvIndex == -1)
        {
            unselectedWarning.SetActive(true);
            return;
        }

        SetToInactive();
    }

    private void SetToInactive()
    {
        envPanel.SetActive(false);
    }

}
