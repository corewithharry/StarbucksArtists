using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementsSwitcher : MonoBehaviour
{
    public PageManager pageManager;
    public TimeManager timeManager;
    public Image image;
    private GameObject[] children;


    private void Update()
    {
        if (transform.tag == "Home Button") // ホームボタンの表示・非表示.
        {
            if (pageManager.currentPage <= 0)
                image.enabled = false;
            else
                image.enabled = true;
        }
        else if (transform.tag == "Header")// ロゴとタイトルの表示・非表示.
        {
            if (pageManager.currentPage == 0)
            {
                image.enabled = true;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else
            {
                image.enabled = false;
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// ホームボタンの挙動.
    /// </summary>
    public void OnClick()
    {
        if (pageManager.isBusy)
            return;

        pageManager.JumpToSpecificArtist(0);
        pageManager.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
    }
}
