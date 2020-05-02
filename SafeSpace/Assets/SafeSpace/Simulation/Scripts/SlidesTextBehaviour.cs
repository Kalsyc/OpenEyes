using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Handles content within slides in the simulator
/// </summary>
public class SlidesTextBehaviour : MonoBehaviour
{
    public string[] title;
    public string[] body;
    public GameObject slideTitleObject;
    public GameObject slideBodyObject;
    public GameObject slideBackgroundObject;
    public GameObject reference;
    public UnityEvent endingEvent;

    private Text slideTitle;
    private Text slideBody;

    private int currentSlide = 0;
    private int numOfSlides;

    void Start()
    {
        slideTitle = slideTitleObject.GetComponent<Text>();
        slideBody = slideBodyObject.GetComponent<Text>();
        numOfSlides = Mathf.Min(title.Length, body.Length);
    }

    public void ChangeSlide()
    {
        Debug.Log("Slide changed");
        if (currentSlide == numOfSlides)
        {
            reference.SetActive(false);
            return;
        }
        Debug.Log("ChangeSlide");
        slideTitle.text = title[currentSlide];
        slideBody.text = body[currentSlide];
        currentSlide++;
    }
}
