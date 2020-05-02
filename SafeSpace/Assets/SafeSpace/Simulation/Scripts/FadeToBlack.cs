using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Fades the scene to black
/// </summary>
public class FadeToBlack : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    public void FadeToNextLevel() 
    {
        FadeTo((int)SceneIndexes.SIMULATION_TWO_SCREEN);
    }

    public void FadeToMeditation()
    {
        FadeTo((int)SceneIndexes.SIMULATION_END_SCREEN);
    }

    public void FadeTo(int levelIndex) 
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeToBlack");
        Debug.Log("fadeto triggered");
    }

    public void OnFadeComplete() 
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
