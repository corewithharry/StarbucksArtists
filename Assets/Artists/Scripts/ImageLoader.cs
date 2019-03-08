using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public int pageID;
    public string url;
    private Rect sourceImageSize = new Rect(0, 0, 800, 600);
    public Image[] image;
    public Image loadingPanel;
    private Text loadingText;
    private bool isComplete;


    private void Start()
    {
        StartCoroutine("LoadImages");
        // このスクリプトは各ページに付いているが，１ページ目の画像のロード状況のみを見て，Loadingメッセージの表示を切り替えている.
        // 余裕があれば，全ての画像のロードが完了してからメッセージを消すようにした方が良い.
        if (loadingPanel != null)
            loadingText = loadingPanel.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (!isComplete && loadingText != null)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time * 3, 1));
        }
    }

    /// <summary>
    /// 各ページの画像をロードする.
    /// </summary>
    private IEnumerator LoadImages()
    {
        // Start a download of the given URL
        using (WWW www = new WWW(url))
        {
            // Wait for download to complete
            yield return www;

            // assign texture
            for (int i = 0; i < image.Length; i++)
            {
                image[i].sprite = Sprite.Create(www.texture, sourceImageSize, Vector2.zero);
                if (i == 0)
                    image[i].GetComponent<ArtistsIndex>().pageID = pageID;
            }
            // １ページ目の画像のロードが完了したら，Loadingメッセージを非表示にする.
            if (loadingPanel != null)
            {
                loadingPanel.gameObject.SetActive(false);
                isComplete = true;
            }
        }
    }
}