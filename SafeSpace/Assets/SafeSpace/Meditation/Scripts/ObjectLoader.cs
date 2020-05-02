using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the appropriate objects in meditation scene so they do not appear in menu
/// </summary>
public class ObjectLoader : MonoBehaviour
{
    public GameObject objectsToLoad;
    public GameObject toInstantiate;
    public GameObject breathingSet;
    public GameObject meditationSet;

    public void Start()
    {
        SceneManager.sceneUnloaded += OnUnloadMenu;
    }

    public void OnUnloadMenu(Scene scene)
    {
        if (objectsToLoad != null)
        {
            objectsToLoad.SetActive(true);
        }
        else
        {
            Debug.Log("Objects to load cannot be found... searching...");
            objectsToLoad = GameObject.FindGameObjectWithTag("ToLoad");
            if (objectsToLoad != null)
            {
                objectsToLoad.SetActive(true);
            }
            else
            {
                throw new System.Exception("No objects to load");
            }
        }
        toInstantiate.SetActive(true);
        int mode;
        try
        {
            mode = PlayerPrefs.GetInt("mode");
        }
        catch
        {
            breathingSet.SetActive(true);
            meditationSet.SetActive(true);
            SceneManager.sceneUnloaded -= OnUnloadMenu;
            throw new PlayerPrefsException("No mode");
        }
        if (mode == 0)
        {
            breathingSet.SetActive(true);
        }
        else if (mode == 1)
        {
            meditationSet.SetActive(true);
        }
        SceneManager.sceneUnloaded -= OnUnloadMenu;
    }

}
