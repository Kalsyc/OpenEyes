using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the next screen to load
/// </summary>
public class SimulationLoader : MonoBehaviour
{
    public void LoadScreenOne()
    {
        SceneManager.LoadScene((int)SceneIndexes.SIMULATION_ONE_SCREEN);
    }
}
