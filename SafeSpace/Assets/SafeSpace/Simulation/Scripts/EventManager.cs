using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //using colliders to trigger
    // https://answers.unity.com/questions/652115/multiple-independent-event-and-one-delegate.html
    public delegate void TriggerAction();
    public static event TriggerAction OnDocument;
    public static event TriggerAction OnCalendar;
    public static event TriggerAction OnSlides;

    public void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter triggered");
        if (other.gameObject.CompareTag("Document")) {
            if (OnDocument != null) {
                OnDocument();
            }
        } else if (other.gameObject.CompareTag("Calendar")) {
            if (OnCalendar != null) {
                OnCalendar();
            }
        } 
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (OnSlides != null) {
                OnSlides();
            }
            print("S key was pressed");
        }
    }

}
