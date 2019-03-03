using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackToHome : MonoBehaviour
{
    public SlideShow slideShow;
    Image image;


    private void Start()
    {
        image = this.GetComponent<Image>();
    }

    public void OnClick()
    {
        slideShow.JumpToPage(0);
    }

    void Update()
    {
        if (slideShow.currentPage == 0)
            image.enabled = false;
        else
            image.enabled = true;
    }
}
