using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
    // Start is called before the first frame update
    void Start()
    {
        slideTitle = slideTitleObject.GetComponent<Text>();
        slideBody = slideBodyObject.GetComponent<Text>();
        numOfSlides = Mathf.Min(title.Length, body.Length);
    }

    public void changeSlide()
    {
        if (currentSlide == numOfSlides)
        {
            reference.SetActive(false);
            endingEvent.Invoke();
            return;
        }
        Debug.Log("ChangeSlide");
        slideTitle.text = title[currentSlide];
        slideBody.text = body[currentSlide];
        currentSlide++;
    }

    void OnEnable()
    {
        EventMaster.OnSlides += changeSlide;
    }

    /*

    public void changeToSlide1() {
        currentSlide = 1;
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.white;
        slideTitle.GetComponent<Text>().text = slide1[0];
        slideBody.GetComponent<Text>().text = slide1[1];
    }

    public void changeToSlide2() {
        currentSlide = 2;
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.white;
        slideTitle.GetComponent<Text>().text = slide2[0];
        slideBody.GetComponent<Text>().text = slide2[1];
    }

    public void changeToSlide3() {
        currentSlide = 3;
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.white;
        slideTitle.GetComponent<Text>().text = slide3[0];
        slideBody.GetComponent<Text>().text = slide3[1];
    }

    public void changeToSlide0() {
        currentSlide = 0;
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.black;
        slideTitle.GetComponent<Text>().text = "";
        slideBody.GetComponent<Text>().text = "";
    }

    public void incrementSlide() {
        currentSlide += 1;
        Debug.Log("Increment slide called, currentSlide: " + currentSlide);
        getSlide();
    }

    public void getSlide() {
        if (currentSlide == 0) {
            changeToSlide0();
        } else if (currentSlide == 1) {
            changeToSlide1();
        } else if (currentSlide == 2) {
            changeToSlide2();
        } else {
            changeToSlide3();
        }
    }
    */
}
