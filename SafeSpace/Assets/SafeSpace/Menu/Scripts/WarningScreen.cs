using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreen : MonoBehaviour
{
    public void GoToMenu()
    {
        Debug.Log("Go to Menu");
        SceneManager.LoadScene((int)SceneIndexes.MENU_SCREEN);
        PlayerPrefs.SetInt("next", (int)SceneIndexes.MENU_SCREEN);
    }

    public void QuitApplication()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
