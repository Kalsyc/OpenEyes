using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SimulationLoader : MonoBehaviour
{
    public void LoadScreenOne()
    {
        SceneManager.LoadScene((int)SceneIndexes.SIMULATION_ONE_SCREEN);
    }
}
