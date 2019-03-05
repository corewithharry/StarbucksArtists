using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeSinceLastInput;
    public float timeUntilAutoScroll = 10f;
    public SlideShow slideShow;


    void Update()
    {
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
