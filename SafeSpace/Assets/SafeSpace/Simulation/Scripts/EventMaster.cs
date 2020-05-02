using UnityEngine;

/// <summary>
/// Defines the events and its functions that will occur in simulator
/// </summary>
public class EventMaster : MonoBehaviour
{
    
    public delegate void TriggerAction();
    public static event TriggerAction OnDocument;
    public static event TriggerAction OnCalendar;
    public static event TriggerAction OnSlideOne;
    public static event TriggerAction OnSlideTwo;
    public static event TriggerAction OnSlideThree;
    public static event TriggerAction OnSlideEnd;
    public static event TriggerAction OnBoot;
    public static event TriggerAction OnSimBoot;
    public static event TriggerAction OnDialogue;

    // Start is called before the first frame update

    public void InvokeOnSimBoot()
    {
        OnSimBoot();
    }

    public void InvokeOnBoot()
    {
        OnBoot();
    }

    public void InvokeOnCalendar()
    {
        OnCalendar();
    }

    public void InvokeOnSlideOne()
    {
        OnSlideOne();
    }
    public void InvokeOnSlideTwo()
    {
        OnSlideTwo();
    }
    public void InvokeOnSlideThree()
    {
        OnSlideThree();
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
