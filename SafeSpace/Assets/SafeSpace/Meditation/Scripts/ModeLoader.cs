using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles interactions for the mode loader in meditation menu
/// </summary>
public class ModeLoader : MonoBehaviour
{
    private int menuEnvIndex = (int)SceneIndexes.MEDITATION_MENU;

    public void UnloadCurrentScene()
    {
        GameObject.FindGameObjectWithTag("OnEnable").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("OnEnable").transform.GetChild(0).gameObject.GetComponent<ObjectLoader>().enabled = true;
        SceneManager.UnloadSceneAsync(menuEnvIndex);

    }
}
