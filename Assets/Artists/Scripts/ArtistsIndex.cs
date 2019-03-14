using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtistsIndex : MonoBehaviour
{
    public int pageID;
    public PageManager pageManager;
    public TimeManager timeManager;


    /// <summary>
    /// アーティスト一覧画面から，各アーティストのページへ飛ぶ.
    /// </summary>
    public void OnImageClick()
    {
        if (pageManager.isBusy)
            return;

        pageManager.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
        pageManager.JumpToSpecificArtist(pageID);
    }
}