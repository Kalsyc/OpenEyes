using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Select Environment One");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
