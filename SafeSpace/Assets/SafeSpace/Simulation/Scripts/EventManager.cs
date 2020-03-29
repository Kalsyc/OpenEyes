using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //using colliders to trigger
    // https://answers.unity.com/questions/652115/multiple-independent-event-and-one-delegate.html
    public delegate void TriggerAction(Collider other);
    public static event TriggerAction OnDocument;
    public static event TriggerAction OnCalendar;
    public static event TriggerAction OnSlides;

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Document")) {
            Debug.Log("OnTriggerEnter triggered");
            if (OnDocument != null) {
                OnDocument(other);
            }
        } else if (other.gameObject.CompareTag("Calendar")) {
            if (OnCalendar != null) {
                OnCalendar(other);
            }
        } else if (other.gameObject.CompareTag("Slides")) {
            if (OnSlides != null) {
                OnSlides(other);
            }
        }
    }
}
