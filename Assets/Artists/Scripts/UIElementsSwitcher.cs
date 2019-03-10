using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElementsSwitcher : MonoBehaviour
{
    public SlideShow slideShow;
    public TimeManager timeManager;
    public Image image;
    public Text text;


    public void Start()
    {
        image = GetComponent<Image>();
        if(image == null)
            text = GetComponent<Text>();
    }

    private void Update()
    {
            if (gameObject.name == "Home Button") // ホームボタンの表示・非表示.
            {
                if (slideShow.currentPage <= 0)
                    image.enabled = false;
                else
                    image.enabled = true;
            }
            else // ロゴとタイトルの表示・非表示.
            {
                if (slideShow.currentPage == 0)
                {
                    if (image != null)
                        image.enabled = true;
                    else
                        text.enabled = true;
                }
                else
                {
                    if (image != null)
                        image.enabled = false;
                    else
                        text.enabled = false;
                }
        }
    }

    /// <summary>
    /// ホームボタンの挙動.
    /// </summary>
    public void OnClick()
    {
        if (slideShow.isBusy)
            return;

        slideShow.JumpToSpecificArtist(0);
        slideShow.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
    }
}
