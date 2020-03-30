using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHighlightShowText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject textToShow;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Highlighted");
        textToShow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Not highlighted anymore");
        textToShow.SetActive(false);
    }
}
