using UnityEngine;

/// <summary>
/// Handles interactions for the intro screen in meditation menu
/// </summary>
public class IntroLoader : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject nextPanel;

    public void LoadNext()
    {
        introPanel.SetActive(false);
        nextPanel.SetActive(true);
    }

}
