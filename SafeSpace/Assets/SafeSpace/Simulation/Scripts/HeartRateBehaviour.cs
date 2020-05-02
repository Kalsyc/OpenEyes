using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles audio for heartrate
/// </summary>
public class HeartRateBehaviour : MonoBehaviour
{
    public AudioSource level1;
    public AudioSource level2;
    public AudioSource level3;
    public AudioSource level4;

    public UnityEvent startEvent;
    void Start()
    {
        PlayHeartRateLvl1();
        StartCoroutine(Delay());
        startEvent.Invoke();
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);
    }

     void OnEnable()
    {
        level1.Stop();
        level2.Stop();
        level3.Stop();
        level4.Stop();
        startEvent.Invoke();
    }

    public void PlayHeartRateLvl1() {
        Debug.Log("Playing HR lvl1");
        level2.Stop();
        level3.Stop();
        level4.Stop();

        level1.Play();
    }

    public void PlayHeartRateLvl2() {
        Debug.Log("Playing HR lvl2");
        level1.Stop();
        level3.Stop();
        level4.Stop();

        level2.Play();
    }

    public void PlayHeartRateLvl3() {
        Debug.Log("Playing HR lvl3");
        level1.Stop();
        level2.Stop();
        level4.Stop();

        level3.Play();
    }

    public void PlayHeartRateLvl4() {
        Debug.Log("Playing HR lvl4");
        level1.Stop();
        level2.Stop();
        level3.Stop();
        
        level4.Play();
    }

}
