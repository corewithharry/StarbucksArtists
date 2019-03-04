using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private float touchStartPos;
    private float touchEndPos;
    string Direction;
    public SlideShow slideShow;
    public BackToHome backToHome;


    private void Start()
    {
        slideShow = this.GetComponent<SlideShow>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = Input.mousePosition.x;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = Input.mousePosition.x;
            GetDirection();
        }
    }

    private void GetDirection()
    {
        float directionX = touchEndPos - touchStartPos;

        if (30f < directionX)
        {
            // 右向きにスワイプ.
            slideShow.autoScrollMode = false;
            backToHome.timeSinceLastInput = 0;
            slideShow.TurnPage(false);
        }
        else if (-30f > directionX)
        {
            // 左向きにスワイプ.
            slideShow.autoScrollMode = false;
            backToHome.timeSinceLastInput = 0;
            slideShow.TurnPage(true);
        }
    }
}
