using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicLoader : MonoBehaviour
{
    public GameObject selectMessage;
    public GameObject musicPanel;
    public GameObject nextPanel;

    public string[] trackNames;

    public string messageBeginning;
    public string messageNoneSelected;

    private bool selected = false;
    private int selectedMusic;
    private Text messageShown;

    public void Start()
    {
        messageShown = selectMessage.GetComponent<Text>();
        messageShown.text = messageNoneSelected;
    }

    public void LoadNext()
    {
        nextPanel.SetActive(true);
        musicPanel.SetActive(false);
    }

    public void UpdateMusic(int id)
    {
        if (selected && id == selectedMusic)
        {
            selected = false;
            messageShown.text = messageNoneSelected;
        }
        else
        {
            selectedMusic = id;
            selected = true;
            messageShown.text = messageBeginning + trackNames[id];
        }
    }
}
