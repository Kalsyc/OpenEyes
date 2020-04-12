using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSettings : MonoBehaviour
{
    public void UpdateMode(int id)
    {
        PlayerPrefs.SetInt("mode", id);
    }

    public int GetMode()
    {
        return PlayerPrefs.GetInt("mode");
    }
}
