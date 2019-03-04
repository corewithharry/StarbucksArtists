using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleImages : MonoBehaviour, IPointerClickHandler
{
    public int pageID;
    public SlideShow slideShow;
    public BackToHome backToHome;

    public void OnPointerClick(PointerEventData eventData)
    {
        slideShow.JumpToPage(pageID);
        backToHome.timeSinceLastInput = 0;
    }
}