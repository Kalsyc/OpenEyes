using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMaster : MonoBehaviour
{
    
    public delegate void TriggerAction();
    public static event TriggerAction OnDocument;
    public static event TriggerAction OnCalendar;
    public static event TriggerAction OnSlides;
    public static event TriggerAction OnSlideEnd;
    public static event TriggerAction OnBoot;
    public static event TriggerAction OnDialogue;

    // Start is called before the first frame update

    public void InvokeOnBoot()
    {
        OnBoot();
    }

    public void InvokeOnCalendar()
    {
        OnCalendar();
    }

    public void InvokeOnSlides()
    {
        OnSlides();
    }

    public void InvokeOnSlideEnd()
    {
        OnSlideEnd();
    }    

    public void InvokeOnDocument()
    {
        OnDocument();
    }
}
