using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
