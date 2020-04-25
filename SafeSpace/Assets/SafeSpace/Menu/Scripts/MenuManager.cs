using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Tobii.G2OM;
using HTC.UnityPlugin.Vive;

public class MenuManager : MonoBehaviour
{
    public GameObject pointer;
    public GameObject quitScreen;
    public GameObject menuScreen;

    private bool showQuit = false;

    public void LoadMeditation()
    {
        pointer.GetComponent<NextLevelPointer>().SetPointer(SceneIndexes.MEDITATION_MENU);
        pointer.GetComponent<NextLevelPointer>().SetCurrent(SceneIndexes.MENU_SCREEN);
        LoadLoadingScreen();
    }

    public void LoadSimulation()
    {
        pointer.GetComponent<NextLevelPointer>().SetPointer(SceneIndexes.SIMULATION_START_SCREEN);
        pointer.GetComponent<NextLevelPointer>().SetCurrent(SceneIndexes.MENU_SCREEN);
        LoadLoadingScreen();
    }

    private void Update()
    {
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Menu))
        {
            if (!showQuit)
            {
                quitScreen.SetActive(true);
                menuScreen.SetActive(false);
                showQuit = true;
            }
            else
            {
                quitScreen.SetActive(false);
                menuScreen.SetActive(true);
                showQuit = false;
            }
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    private void LoadLoadingScreen()
    {
        SceneManager.LoadScene((int)SceneIndexes.LOADING_SCREEN);
        //SceneManager.LoadSceneAsync((int)SceneIndexes.LOADING_SCREEN, LoadSceneMode.Additive);
    }
}
