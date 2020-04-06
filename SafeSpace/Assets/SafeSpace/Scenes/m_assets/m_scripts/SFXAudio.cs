using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource selectedGameAudio;

    // Start is called before the first frame update
    void Start()
    {
        selectedGameAudio = gameObject.GetComponent<AudioSource>();
    }

    public void selectAudio (int id)
    {
        Debug.Log("selected: " + id);
        selectedGameAudio.clip = audioClips[id];
        selectedGameAudio.Play();
        selectedGameAudio.time = 100f;
        Debug.Log("isPlaying: " + selectedGameAudio.isPlaying);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
