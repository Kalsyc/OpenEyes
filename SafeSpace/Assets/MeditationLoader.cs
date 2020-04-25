using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
