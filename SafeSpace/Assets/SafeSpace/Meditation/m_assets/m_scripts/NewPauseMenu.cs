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
    public GameObject settingsPage;

    private static bool inGame = false;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (ViveInput.GetPressDown(HandRole.RightHand, ControllerButton.Menu) && inGame)
        {
            if (GameIsPaused)
            {
                Resume();
            } 
            else
            {
                Pause();
            }
        }
    }

    public static void ToggleInGame()
    {
        inGame = true;
    }

    public void ToggleSettings()
    {
        Debug.Log("Go to settings");
        pauseMenuUI.SetActive(false);
        settingsPage.SetActive(true);
    }

    public void Resume ()
    {
        Debug.Log("Resuming Game");
        settingsPage.SetActive(false);
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


    void Pause()
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
        Time.timeScale = 1f;
        AudioListener.pause = false;
        inGame = false;
        SceneManager.LoadScene((int)SceneIndexes.MENU_SCREEN);
        Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        Debug.Log("Loading Menu...");
        PlayerPrefs.SetInt("mode", -1);
    }

    public void QuitGame()
    {
        AudioListener.pause = false;
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

}

