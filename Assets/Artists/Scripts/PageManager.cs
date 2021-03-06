﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class PageManager : MonoBehaviour
{
    private SwipeGesture swipeGesture;
    public Tween moveAnimation;

    private int numArtworks;
    private RectTransform rectTransform;
    public int pageWidth = 2048;
    public int currentPage = 1;

    public TimeManager timeManager;
    public bool autoScrollMode;
    private float elapsedTime;
    public float transitionTime = 10f;
    public float autoScrollSpeed = 1.0f;
    private bool pageTurned;
    private bool isAscending;
    public bool isBusy;
    // 一覧ページの下限. アンカーポイントが左上なので，ページの縦の長さがheight(1536) * nの場合，height * (n-1)を指定すること.
    public Vector2 lowerLimit = new Vector2(0, 1536 * 3);
    public float scrollAmount = 1500f;


    void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None; // Tween生成時に自動再生させない.

        numArtworks = Artworks.Instance.numOnDisplay;
    }

    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        swipeGesture = GetComponent<SwipeGesture>();

        // 各ページ間の水平スクロール.
        // 左スワイプ（進む）.
        swipeGesture
            .OnSwipeLeft
            .Where(_ => currentPage < numArtworks) // 最大ページ以前である場合のみ進める.
            .Where(_ => currentPage != 0) // 選択画面ではスワイプさせない.
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying()) // アニメーション実行中ではない.
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                currentPage++;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x - pageWidth, 0.5f)
                .Play();
            });

        // 右スワイプ（戻る）.
        swipeGesture
            .OnSwipeRight
            .Where(_ => currentPage > 1) // 1ページ目以降である場合のみ戻れる.
            .Where(_ => currentPage != 0)
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                currentPage--;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x + pageWidth, 0.5f)
                .Play();
            });

        // 最終ページ.
        swipeGesture
            .OnSwipeLeft
            .Where(_ => currentPage >= numArtworks) // これ以上は進めない.
            .Where(_ => currentPage != 0)
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOShakeAnchorPos(0.5f, Vector3.right * 200, 10)
                .Play();
            });

        // １ページ目.
        swipeGesture
            .OnSwipeRight
            .Where(_ => currentPage <= 1) // これ以上は戻れない.
            .Where(_ => currentPage != 0)
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOShakeAnchorPos(0.5f, Vector3.left * 200, 10)
                .Play();
            });

        // 一覧ページでの垂直スクロール.
        // 上スワイプ（下降）.
        swipeGesture
            .OnSwipeUp
            .Where(_ => rectTransform.anchoredPosition.y < lowerLimit.y)
            .Where(_ => currentPage == 0)
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOAnchorPosY(rectTransform.anchoredPosition.y + scrollAmount, 0.8f)
                .Play();
            });
        // 下スワイプ（上昇）.
        swipeGesture
            .OnSwipeDown
            .Where(_ => rectTransform.anchoredPosition.y > 0)
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOAnchorPosY(rectTransform.anchoredPosition.y - scrollAmount, 0.8f)
                .Play();
            });

        // 下限.
        swipeGesture
            .OnSwipeUp
            .Where(_ => rectTransform.anchoredPosition.y >= lowerLimit.y)
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOShakeAnchorPos(0.5f, Vector3.up * 200, 10)
                .Play();
            });

        // 上限.
        swipeGesture
            .OnSwipeDown
            .Where(_ => rectTransform.anchoredPosition.y <= 0)
            .Where(_ => moveAnimation == null || !moveAnimation.IsPlaying())
            .Subscribe(_ =>
            {
                autoScrollMode = false;
                timeManager.timeSinceLastInput = 0;
                moveAnimation = rectTransform
                .DOShakeAnchorPos(0.5f, Vector3.down * 200, 10)
                .Play();
            });
    }

    void Start()
    {
        JumpToSpecificArtist(Artworks.Instance.selectedWorkID);
        autoScrollMode = true;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (autoScrollMode)
        {
            if ((elapsedTime >= transitionTime) && (!pageTurned))
            {
                AutoScroll();
            }
        }

        isBusy = moveAnimation.IsPlaying();

        if (currentPage > 0)
            return;

        if (rectTransform.anchoredPosition.y <= 0)
            rectTransform.anchoredPosition = Vector2.zero;

        if (rectTransform.anchoredPosition.y >= lowerLimit.y)
            rectTransform.anchoredPosition = lowerLimit;
    }

    public void JumpToSpecificArtist(int pageID)
    {
        // 位置をリセット.
        moveAnimation = rectTransform
        .DOAnchorPosX(0f, 0f)
        .Play();
        rectTransform.anchoredPosition = Vector2.zero;
        currentPage = 0;

        if (pageID == 0)
            return;
            
        moveAnimation = rectTransform
        .DOAnchorPosX(rectTransform.anchoredPosition.x - pageWidth * pageID, 0f)
        .Play();
        currentPage = pageID;
    }

    public void AutoScroll()
    {
        pageTurned = true;
        if (isAscending)
        {
            if (currentPage < numArtworks)
            {
                currentPage++;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x - pageWidth, autoScrollSpeed)
                .Play();
            }
            else if (currentPage >= numArtworks)
            {
                currentPage--;
                isAscending = false;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x + pageWidth, autoScrollSpeed)
                .Play();
            }

        }
        else if(!isAscending)
        {
            if (currentPage > 1)
            {
                currentPage--;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x + pageWidth, autoScrollSpeed)
                .Play();
            }
            else if (currentPage <= 1)
            {
                currentPage++;
                isAscending = true;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x - pageWidth, autoScrollSpeed)
                .Play();
            }
        }

        elapsedTime = 0;
        pageTurned = false;
    }
}
