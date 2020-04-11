using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class musicPanel : MonoBehaviour
{
    public void SetToActive()
    {
        gameObject.SetActive(true);
    }

    public void Unload()
    {
        SceneManager.UnloadSceneAsync((int)SceneIndexes.MEDITATION_MENU);
    }
}
