using UnityEngine;

/// <summary>
/// Controls sound effect audio
/// </summary>
public class SFXAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource selectedGameAudio;
    private bool selected = false;
    private int selectedAudio;

    void Start()
    {
        selectedGameAudio = gameObject.GetComponent<AudioSource>();
    }

    public void selectAudio (int id)
    {
        if (selected && id == selectedAudio)
        {
            Debug.Log("unselected: " + id);
            selected = false;
            selectedGameAudio.Stop();
        }
        else
        {
            selected = true;
            selectedAudio = id;
            Debug.Log("selected: " + id);
            selectedGameAudio.clip = audioClips[id];
            selectedGameAudio.Play();
            selectedGameAudio.time = 100f;
            Debug.Log("isPlaying: " + selectedGameAudio.isPlaying);
            return;
        }
    }

}
