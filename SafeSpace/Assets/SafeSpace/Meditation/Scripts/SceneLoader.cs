using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles loading of scenes in meditation
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public void GoToGrass()
    {
        SceneManager.LoadScene("m_Env_Grass");
    }

    public void GoToFlowers()
    {
        SceneManager.LoadScene("m_Env_Rocks");
    }
}
