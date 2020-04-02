using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeartRateBehaviour : MonoBehaviour
{
    public int heartRateLevel;
    public AudioSource level1;
    public AudioSource level2;
    public AudioSource level3;
    public AudioSource level4;
    // Start is called before the first frame update
    void Start()
    {
        heartRateLevel = 0;
    }

     void OnEnable()
    {
        level1.Stop();
        level2.Stop();
        level3.Stop();
        level4.Stop();
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

    // void OnDisable()
    // {
    //     EventManager.OnCalendar -= IncreaseHeartRate;
    //     EventManager.OnDocument -= IncreaseHeartRate;
    //     EventManager.OnSlides -= IncreaseHeartRate;
    // }




    // 4 ish levels
    // Do it by actually incrementing it each time.
    // level 1: initial 
    // level 2: calendar(because presentation is gonna happen)
    // level 3: slides (gibberish)
    // level 4: document (discouragement) 
    // public void IncreaseHeartRate() {
    //     Debug.Log("Increase Heart rate triggered");
    //     heartRateLevel += 1;
    //     if (heartRateLevel == 1) {
    //         level2.Stop();
    //         level3.Stop();
    //         level4.Stop();

    //         level1.Play();
    //     } else  if (heartRateLevel == 2) {
    //         level1.Stop();
    //         level3.Stop();
    //         level4.Stop();

    //         level2.Play();
    //     } else if (heartRateLevel == 3) {
    //         level1.Stop();
    //         level2.Stop();
    //         level4.Stop();

    //         level3.Play();
    //     } else if (heartRateLevel >= 4) {
    //         level1.Stop();
    //         level2.Stop();
    //         level3.Stop();

    //         level4.Play();
    //     }
    // }

    

    // void FixedUpdate() {
    //     // Debug.Log("fixed update");
    // }

}
