using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Decides which level to load based on NextLevelPointer. This is a workaround to avoid having different loading screens
/// </summary>
public class LevelLoader : MonoBehaviour
{
    private GameObject pointer;
    private int toLoad;

    void Start()
    {
        while (pointer == null)
        {
            pointer = GameObject.FindGameObjectWithTag("NextLevelPointer");
        }
        Debug.Log(pointer);
        toLoad = (int)pointer.GetComponent<NextLevelPointer>().GetPointer();


    }

    public void LoadLevel()
    {
        Destroy(pointer);
        SceneManager.LoadScene(toLoad);
    }

}
