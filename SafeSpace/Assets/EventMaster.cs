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
    

    // Start is called before the first frame update
    
    public void InvokeOnCalendar()
    {
        OnCalendar();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
