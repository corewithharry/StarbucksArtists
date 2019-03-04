using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideShow : MonoBehaviour
{
    public int numArtists = 2;
    public int pageWidth = 2048;
    public int pageHeight = 2732;
    private RectTransform rectTransform;
    private Vector3[] pagePos;

    public bool autoScrollMode;
    public float transitionTime = 3f;
    public float time;
    private bool pageTurned;
    public int currentPage = 1;
    public KeyCode debugKey = KeyCode.Space;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        pagePos = new Vector3[numArtists + 1];
        for (int i = 0; i <= numArtists; i++)
        {
            pagePos[i] = new Vector3(0 - pageWidth * i, pageHeight, 0);
        }

        autoScrollMode = true;
        JumpToPage(1);
    }

    void Update()
    {
        time += Time.deltaTime;

        if (autoScrollMode)
        {
            if (((time >= transitionTime) && (!pageTurned)) || (Input.GetKeyDown(debugKey)))
            {
                TurnPage();
            }
        }
    }

    public void JumpToPage(int pageID)
    {
        currentPage = pageID;
        rectTransform.position = pagePos[currentPage];
    }

    void TurnPage()
    {
        pageTurned = true;

        if (currentPage < numArtists)
        {
            currentPage++;
        }
        else
        {
            currentPage = 1;
        }
        rectTransform.position = pagePos[currentPage];

        time = 0;
        pageTurned = false;
    }
}