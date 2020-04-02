using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedBehaviour : MonoBehaviour
{
    public List<UnityEvent> events;
    public List<float> delays;
    public string eventType;

    private void OnEnable() {
        if (eventType == "Document") {
            EventManager.OnDocument += Orchestrate;
        } else if (eventType == "Slides") {
            EventManager.OnSlides += Orchestrate;
        } else if (eventType == "Calendar") {
            EventManager.OnCalendar += Orchestrate;
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
