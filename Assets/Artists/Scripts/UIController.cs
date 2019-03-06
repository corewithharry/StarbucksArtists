using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public SlideShow slideShow;
    public TimeManager timeManager;
    public Image image;
    public Text text;
    private string name;


    public void Start()
    {
        image = GetComponent<Image>();
        if(image == null)
            text = GetComponent<Text>();
        name = gameObject.name;
    }

    private void Update()
    {
        if (slideShow.currentPage == 0)
        {
            if (name == "Home Button")
            {
                image.enabled = false;
            }
            else
            {
                if (image != null)
                    image.enabled = true;
                else
                    text.enabled = true;
            }
        }
        else
        {
            if (name == "Home Button")
            {
                image.enabled = true;
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

    public void OnClick()
    {
        if (slideShow.isBusy)
        {
            return;
        }

        slideShow.JumpToSpecificArtist(0);
        slideShow.autoScrollMode = false;
        timeManager.timeSinceLastInput = 0;
    }
}
