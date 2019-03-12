using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class IndexManager : MonoBehaviour
{
    private RectTransform rectTransform;
    public int pageHeight = 3072;

    private SwipeGesture swipeGesture;
    private Tween moveAnimation;

    public TimeManager timeManager;
    public bool isBusy;

    private Vector2 pos;


    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        swipeGesture = GetComponent<SwipeGesture>();

        // next
        swipeGesture
            .OnSwipeDown
            .Where(_ => rectTransform.anchoredPosition.y > 0) // 最大ページ以前である場合のみ進める.
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying()) // アニメーション実行中ではない.
            .Subscribe(_ =>
            {
                //timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOAnchorPosY(rectTransform.anchoredPosition.y - 1500, 1f)
                .Play();
            });

        // back
        swipeGesture
            .OnSwipeUp
            .Where(_ => rectTransform.anchoredPosition.y < pageHeight) // 1ページ目以降である場合のみ戻れる.
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                //timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOAnchorPosY(rectTransform.anchoredPosition.y + 1500, 1f)
                .Play();
            });

        // last page
        swipeGesture
            .OnSwipeDown
            .Where(_ => rectTransform.anchoredPosition.y <= 0) // これ以上は進めない.
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                //timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOShakeAnchorPos(0.5f, Vector3.down * 200, 10)
                .Play();
            });

        //// 1st page
        swipeGesture
            .OnSwipeUp
            .Where(_ => rectTransform.anchoredPosition.y >= pageHeight) // これ以上は戻れない.
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                //timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOShakeAnchorPos(0.5f, Vector3.up * 200, 10)
                .Play();
            });
    }

    void Update()
    {
        //elapsedTime += Time.deltaTime;

        //if (autoScrollMode)
        //{
        //    if ((elapsedTime >= transitionTime) && (!pageTurned))
        //    {
        //        AutoScroll();
        //    }
        //}

        //isBusy = moveAnimation.IsPlaying();
    }

    public void AutoScroll()
    {
        //pageTurned = true;
        //if (isAscending)
        //{
        //    if (currentPage < numArtists)
        //    {
        //        currentPage++;
        //        moveAnimation = rectTransform
        //        .DOAnchorPosX(rectTransform.anchoredPosition.x - pageHeight, autoScrollSpeed)
        //        .Play();
        //    }
        //    else if (currentPage >= numArtists)
        //    {
        //        currentPage--;
        //        isAscending = false;
        //        moveAnimation = rectTransform
        //        .DOAnchorPosX(rectTransform.anchoredPosition.x + pageHeight, autoScrollSpeed)
        //        .Play();
        //    }

        //}
        //else if (!isAscending)
        //{
        //    if (currentPage > 1)
        //    {
        //        currentPage--;
        //        moveAnimation = rectTransform
        //        .DOAnchorPosX(rectTransform.anchoredPosition.x + pageHeight, autoScrollSpeed)
        //        .Play();
        //    }
        //    else if (currentPage <= 1)
        //    {
        //        currentPage++;
        //        isAscending = true;
        //        moveAnimation = rectTransform
        //        .DOAnchorPosX(rectTransform.anchoredPosition.x - pageHeight, autoScrollSpeed)
        //        .Play();
        //    }
        //}

        //elapsedTime = 0;
        //pageTurned = false;
    }
}
