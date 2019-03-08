using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    public int pageID;
    public string url;
    private Rect sourceImageSize = new Rect(0, 0, 800, 600);
    public Image[] image;
    public Image loading;


    private IEnumerator Start()
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
            // 画像のロードが完了したら，Loadingメッセージを非表示にする.
            if(loading != null)
                loading.gameObject.SetActive(false);
        }
    }
}