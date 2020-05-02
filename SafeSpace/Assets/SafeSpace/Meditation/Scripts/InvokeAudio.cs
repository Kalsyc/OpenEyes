using UnityEngine;

/// <summary>
/// Handles audio for guided breathing voiceover
/// </summary>
public class InvokeAudio : MonoBehaviour
{
    private AudioSource audioSource;
    private bool onPlay = false;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Meditation").GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if (onPlay && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play(0);
        onPlay = true;
    }
}
