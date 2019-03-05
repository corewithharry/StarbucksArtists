using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SwipeGesture), typeof(HorizontalLayoutGroup))]
public class TurnPage : MonoBehaviour
{
    public float PageWidth = 1080;

    private RectTransform rectTransform;
    private SwipeGesture swipeGesture;
    private Tween moveAnimation;

    public int pageCount;
    public int currentPage = 1;


    void Awake()
    {
        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.None; // Tween生成時に自動再生させない
    }

    void OnEnable()
    {
        this.rectTransform = this.GetComponent<RectTransform>();
        this.swipeGesture = this.GetComponent<SwipeGesture>();

        // next
        this.swipeGesture
            .OnSwipeLeft
            .Where(_ => currentPage < pageCount) // 最大ページ以前である場合のみ進める
            .Where(_ => this.moveAnimation == null || !this.moveAnimation.IsPlaying()) // アニメーション実行中ではない
            .Subscribe(_ =>
            {
                this.currentPage++;
                this.moveAnimation = this.rectTransform
                    .DOAnchorPosX(rectTransform.anchoredPosition.x - this.PageWidth, 0.5f)
                    .Play();
            });

        // back
        this.swipeGesture
            .OnSwipeRight
            .Where(_ => currentPage > 1) // 1ページ目以降である場合のみ戻れる
            .Where(_ => this.moveAnimation == null || !this.moveAnimation.IsPlaying()) // アニメーション実行中ではない
            .Subscribe(_ =>
            {
                this.currentPage--;
                this.moveAnimation = this.rectTransform
                    .DOAnchorPosX(rectTransform.anchoredPosition.x + this.PageWidth, 0.5f)
                    .Play();
            });

        // last page
        this.swipeGesture
            .OnSwipeLeft
            .Where(_ => currentPage == pageCount) // これ以上は進めない
            .Where(_ => this.moveAnimation == null || !this.moveAnimation.IsPlaying()) // アニメーション実行中ではない
            .Subscribe(_ =>
            {
                this.moveAnimation = this.rectTransform
                    .DOShakeAnchorPos(0.5f, Vector3.right * 200, 10)
                    .Play();
            });

        // 1st page
        this.swipeGesture
            .OnSwipeRight
            .Where(_ => currentPage == 1) // これ以上は戻れない
            .Where(_ => this.moveAnimation == null || !this.moveAnimation.IsPlaying()) // アニメーション実行中ではない
            .Subscribe(_ =>
            {
                this.moveAnimation = this.rectTransform
                    .DOShakeAnchorPos(0.5f, Vector3.left * 200, 10)
                    .Play();
            });
    }
}