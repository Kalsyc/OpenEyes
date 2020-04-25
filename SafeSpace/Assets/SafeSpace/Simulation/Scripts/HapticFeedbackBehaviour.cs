using Tobii.G2OM;
using UnityEngine;
using System.Collections;
using HTC.UnityPlugin.Vive;

public class HapticFeedbackBehaviour : MonoBehaviour
{
    public float[] intervals;
    public float[] amplitudeLevels;
    public float[] frequencyLevels;
    public float[] durationLevels;

    private bool isVibrating = false;
    private bool toStop = false;
    private int maxLevel;
    private int count = 0;

    private void Start()
    {
        maxLevel = Mathf.Min(intervals.Length, frequencyLevels.Length, amplitudeLevels.Length, durationLevels.Length);
    }

    public void TriggerVibration()
    {
        if (count == 0)
        {
            StartCoroutine(Vibrate());
        }
        if (count == maxLevel)
        {
            return;
        }
        count++;
    }

    private IEnumerator Vibrate()
    {
        while (true)
        {
            ViveInput.TriggerHapticVibration(HandRole.LeftHand, durationLevels[count], frequencyLevels[count], amplitudeLevels[count], 0f);
            yield return new WaitForSeconds(intervals[count]);
        }
    }

}
