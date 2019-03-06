using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    public int numArtists = 2;
    public int pageWidth = 2048;
    private RectTransform rectTransform;
    public int currentPage = 1;

    private SwipeGesture swipeGesture;
    public Tween moveAnimation;

    public TimeManager timeManager;
    public bool autoScrollMode;
    public float transitionTime = 3f;
    private float time;
    private bool pageTurned;
    bool isAscending;


    void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None; // Tween生成時に自動再生させない
    }

    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        swipeGesture = GetComponent<SwipeGesture>();

        // next
        swipeGesture
            .OnSwipeLeft
            .Where(_ => currentPage < numArtists) // 最大ページ以前である場合のみ進める.
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

        // back
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

        // last page
        swipeGesture
            .OnSwipeLeft
            .Where(_ => currentPage >= numArtists) // これ以上は進めない.
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

        // 1st page
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
    }

    void Start()
    {
        JumpToSpecificArtist(1);
        autoScrollMode = true;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (autoScrollMode)
        {
            if ((time >= transitionTime) && (!pageTurned))
            {
                TurnPage();
            }
        }
    }

    public void JumpToSpecificArtist(int pageID)
    {
        // reset position
        moveAnimation = rectTransform
        .DOAnchorPosX(0f, 0f)
        .Play();
        currentPage = 0;

        if (pageID == 0)
            return;
            
        moveAnimation = rectTransform
        .DOAnchorPosX(rectTransform.anchoredPosition.x - pageWidth * pageID, 0f)
        .Play();
        currentPage = pageID;
    }

    public void TurnPage()
    {
        //if (moveAnimation.IsPlaying())
            //return;

        pageTurned = true;
        if (isAscending)
        {
            if (currentPage < numArtists)
            {
                currentPage++;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x - pageWidth, 0.5f)
                .Play();
            }
            else if (currentPage >= numArtists)
            {
                currentPage--;
                isAscending = false;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x + pageWidth, 0.5f)
                .Play();
            }

        }
        else if(!isAscending)
        {
            if (currentPage > 1)
            {
                currentPage--;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x + pageWidth, 0.5f)
                .Play();
            }
            else if (currentPage <= 1)
            {
                currentPage++;
                isAscending = true;
                moveAnimation = rectTransform
                .DOAnchorPosX(rectTransform.anchoredPosition.x - pageWidth, 0.5f)
                .Play();
            }
        }

        time = 0;
        pageTurned = false;
    }
}
