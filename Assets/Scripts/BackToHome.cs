using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackToHome : MonoBehaviour
{
    public SlideShow slideShow;
    Image image; // Home Button
    public float timeSinceLastInput;
    public float timeUntilAutoScroll = 10f;


    private void Start()
    {
        image = this.GetComponent<Image>();
    }

    public void OnClick()
    {
        timeSinceLastInput = 0;
        slideShow.autoScrollMode = false;
        slideShow.JumpToPage(0);
    }

    void Update()
    {
        if (slideShow.currentPage == 0)
            image.enabled = false;
        else
            image.enabled = true;

        CountDownToScroll();
    }

    private void CountDownToScroll()
    {
        if (slideShow.autoScrollMode)
            return;

        timeSinceLastInput += Time.deltaTime;
        if (timeSinceLastInput > timeUntilAutoScroll)
        {
            slideShow.autoScrollMode = true;
            timeSinceLastInput = 0;
        }
    }
}
