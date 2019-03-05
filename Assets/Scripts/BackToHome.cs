using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackToHome : MonoBehaviour
{
    public SlideShow slideShow;
    public TimeManager timeManager;


    public void OnClick()
    {
        slideShow.JumpToSpecificArtist(0);
        slideShow.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
    }
}
