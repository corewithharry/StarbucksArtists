using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistsIndex : MonoBehaviour
{
    public int pageID;
    public SlideShow slideShow;
    public TimeManager timeManager;


    /// <summary>
    /// アーティスト一覧画面から，各アーティストのページへ飛ぶ.
    /// </summary>
    public void OnImageClick()
    {
        slideShow.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
        slideShow.JumpToSpecificArtist(pageID);
    }
}