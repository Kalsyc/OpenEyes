using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class NewPauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Menu))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        Debug.Log("Resuming Game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameIsPaused = false;
    }

    public void Restart()
    {
        Debug.Log("Restarted Game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
        GameIsPaused = false;
    }


    void Pause ()
    {
        Debug.Log("Game is Paused");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
        GameIsPaused = true;
    }
 
    public void LoadMenu()
    {
        // for the main menu. collaborate with arthur later. (Menu) from build settings
        //SceneManagement.LoadScene("Menu");
        Time.timeScale = 1f;
        AudioListener.pause = false;
        Debug.Log("Loading Menu...");
    }

    public void QuitGame()
    {
        AudioListener.pause = false;
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

}

