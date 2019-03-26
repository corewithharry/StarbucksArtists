using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IndexPageBehaviour : MonoBehaviour
{
    public int pageID;
    public PageManager pageManager;
    public TimeManager timeManager;
    private int numOnDisplay;


    private void Start()
    {
        numOnDisplay = Artworks.Instance.numOnDisplay;
    }

    /// <summary>
    /// アーティスト一覧画面から，各アーティストのページへ飛ぶ.
    /// </summary>
    public void OnImageClick()
    {
        if (pageManager.isBusy || pageID > numOnDisplay)
            return;

        pageManager.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
        pageManager.JumpToSpecificArtist(pageID);
    }
}