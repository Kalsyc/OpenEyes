using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseScreen : MonoBehaviour
{
    public void EnableEyeTracking()
    {
        PlayerPrefs.SetInt("eye", 1);
        SceneManager.LoadScene((int)SceneIndexes.WARNING_SCREEN);
    }

    public void DisableEyeTracking()
    {
        PlayerPrefs.SetInt("eye", 0);
        SceneManager.LoadScene((int)SceneIndexes.WARNING_SCREEN);
    }    
}
