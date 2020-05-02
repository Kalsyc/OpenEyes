using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles events in a timed manner
/// </summary>
public class TimedBehaviour : MonoBehaviour
{
    public List<UnityEvent> events;
    public List<float> delays;
    public string eventType;

    private void OnEnable() {
        switch (eventType)
        {
            case "Document":
                EventMaster.OnDocument += Orchestrate;
                break;
            case "SlideOne":
                EventMaster.OnSlideOne += Orchestrate;
                break;
            case "SlideTwo":
                EventMaster.OnSlideTwo += Orchestrate;
                break;
            case "SlideThree":
                EventMaster.OnSlideThree += Orchestrate;
                break;
            case "Calendar":
                EventMaster.OnCalendar += Orchestrate;
                break;
            case "SlideEnd":
                EventMaster.OnSlideEnd += Orchestrate;
                break;
            case "Boot":
                EventMaster.OnBoot += Orchestrate;
                break;
            case "SimBoot":
                EventMaster.OnSimBoot += Orchestrate;
                break;
        }
    }

    private void Orchestrate() {
        Debug.Log("Orchestrate in timedbehaviour triggered");
        var validPairs = Mathf.Min(events.Count, delays.Count);

        for (int i = 0; i < validPairs; i++)
        {
            StartCoroutine(InvokeDelayed(events[i], delays[i]));
        }
    }

    private IEnumerator InvokeDelayed(UnityEvent unityEvent, float delay)
    {
        yield return new WaitForSeconds(delay);

        unityEvent.Invoke();
    }
}
