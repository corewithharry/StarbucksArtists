using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToHome : MonoBehaviour
{
    public SlideShow slideShow;
    public TimeManager timeManager;
    public Image homeButton;

    public void Start()
    {
        homeButton = GetComponent<Image>();
    }

    private void Update()
    {
        if (slideShow.currentPage == 0)
            homeButton.enabled = false;
        else
            homeButton.enabled = true;
    }

    public void OnClick()
    {
        slideShow.JumpToSpecificArtist(0);
        slideShow.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
    }
}
