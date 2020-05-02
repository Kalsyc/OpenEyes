using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Loads the simulator onto the meditation screen
/// </summary>
public class MeditationLoader : MonoBehaviour
{
    public float delay;
    public GameObject animationText;
    public void Start()
    {
        StartCoroutine(ShowText());
    }   
    private IEnumerator ShowText()
    {
        animationText.SetActive(true);
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene((int)SceneIndexes.MEDITATION_MENU);
    }
    
}
