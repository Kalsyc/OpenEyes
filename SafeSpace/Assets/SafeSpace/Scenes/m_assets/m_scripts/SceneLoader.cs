using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
