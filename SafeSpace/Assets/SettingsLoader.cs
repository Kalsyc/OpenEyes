using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject pausePanel;

    public void LoadPause()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
