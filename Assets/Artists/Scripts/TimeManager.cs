using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeSinceLastInput;
    public float timeUntilAutoScroll = 10f;
    public PageManager pageManager;


    void Update()
    {
        CountDownToScroll();
    }

    private void CountDownToScroll()
    {
        if (pageManager.autoScrollMode)
            return;

        timeSinceLastInput += Time.deltaTime;
        if (timeSinceLastInput > timeUntilAutoScroll)
        {
            pageManager.autoScrollMode = true;
            timeSinceLastInput = 0;
        }
    }
}
