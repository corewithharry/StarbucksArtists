using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour, IPointerClickHandler
{
    public int pageID;
    public SlideShow slideShow;


    public void OnPointerClick(PointerEventData eventData)
    {
        slideShow.JumpToPage(pageID);
    }
}