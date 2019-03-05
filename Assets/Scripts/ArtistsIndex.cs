using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistsIndex : MonoBehaviour
{
    public int pageID;
    public SlideShow slideShow;
    public TimeManager timeManager;


    public void OnImageClick()
    {
        slideShow.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
        slideShow.JumpToSpecificArtist(pageID);
    }
}