using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehaviourScript : MonoBehaviour
{
    public string calendarText;
    public string slidesText;
    public string documentText;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = "";
    }

     void OnEnable()
    {
        EventManager.OnCalendar += triggerCalendar;
        EventManager.OnDocument += triggerDocument;
        EventManager.OnSlides += triggerSlides;
    }

    void OnDisable()
    {
        EventManager.OnCalendar -= triggerCalendar;
        EventManager.OnDocument -= triggerDocument;
        EventManager.OnSlides -= triggerSlides;
    }

    public void triggerCalendar(Collider other) {
        this.GetComponent<Text>().text = calendarText;
    }

    public void triggerSlides(Collider other) {
        this.GetComponent<Text>().text = slidesText;
    }
    
    public void triggerDocument(Collider other) {
        this.GetComponent<Text>().text = documentText;
    }    

    void FixedUpdate() {
        // Debug.Log("fixed update");
    }
}
