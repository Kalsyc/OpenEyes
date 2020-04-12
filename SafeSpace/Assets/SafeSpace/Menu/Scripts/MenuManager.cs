using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadMeditation()
    {
        SceneManager.LoadScene((int)SceneIndexes.MEDITATION_MENU);
    }

    public void LoadSimulation()
    {
        SceneManager.LoadScene((int)SceneIndexes.SIMULATION_SCREEN);
    }
}
