using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidesTextBehaviour : MonoBehaviour
{
    public string[] slide1 = new string[2];
    public string[] slide2 = new string[2];
    public string[] slide3 = new string[2];
    GameObject slideTitle;
    GameObject slideBody;
    GameObject slideBackground;
    public int currentSlide;
    // Start is called before the first frame update
    void Start()
    {
        slideTitle = GameObject.Find("Title");
        slideBody = GameObject.Find("SlidesBody");
        slideBackground = GameObject.Find("Background");
        currentSlide = 0;
        getSlide();
    }

    void OnEnable()
    {
        EventManager.OnSlides += incrementSlide;
    }

    public void changeToSlide1() {
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.white;
        slideTitle.GetComponent<Text>().text = slide1[0];
        slideBody.GetComponent<Text>().text = slide1[1];
    }

    public void changeToSlide2() {
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.white;
        slideTitle.GetComponent<Text>().text = slide2[0];
        slideBody.GetComponent<Text>().text = slide2[1];
    }

    public void changeToSlide3() {
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.white;
        slideTitle.GetComponent<Text>().text = slide3[0];
        slideBody.GetComponent<Text>().text = slide3[1];
    }

    public void changeToSlide0() {
        slideBackground.GetComponent<Image>().color = UnityEngine.Color.black;
        slideTitle.GetComponent<Text>().text = "";
        slideBody.GetComponent<Text>().text = "";
    }

    public void incrementSlide() {
        currentSlide += 1;
        getSlide();
    }

    public void getSlide() {
        if (currentSlide == 0) {
            changeToSlide0();
        } else if (currentSlide == 5) {
            changeToSlide1();
        } else if (currentSlide == 10) {
            changeToSlide2();
        } else {
            changeToSlide3();
        }
    }
}
