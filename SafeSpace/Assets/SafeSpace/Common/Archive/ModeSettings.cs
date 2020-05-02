using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSettings : MonoBehaviour
{
    public void Start()
    {
        PlayerPrefs.SetInt("mode", -1);
    }

    public void UpdateMode(int id)
    {
        PlayerPrefs.SetInt("mode", id);
        PauseMenu.ToggleInGame();
    }

    public int GetMode()
    {
        return PlayerPrefs.GetInt("mode");
    }
}
