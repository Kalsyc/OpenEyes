using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject pointer;
    public void LoadMeditation()
    {
        pointer.GetComponent<NextLevelPointer>().SetPointer(SceneIndexes.MEDITATION_MENU);
        pointer.GetComponent<NextLevelPointer>().SetCurrent(SceneIndexes.MENU_SCREEN);
        LoadLoadingScreen();
    }

    public void LoadSimulation()
    {
        pointer.GetComponent<NextLevelPointer>().SetPointer(SceneIndexes.SIMULATION_SCREEN);
        pointer.GetComponent<NextLevelPointer>().SetCurrent(SceneIndexes.MENU_SCREEN);
        LoadLoadingScreen();
    }

    private void LoadLoadingScreen()
    {
        SceneManager.LoadScene((int)SceneIndexes.LOADING_SCREEN);
        //SceneManager.LoadSceneAsync((int)SceneIndexes.LOADING_SCREEN, LoadSceneMode.Additive);
    }
}
