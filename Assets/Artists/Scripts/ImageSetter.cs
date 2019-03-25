using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetter : MonoBehaviour
{
    public Image[] images;
    public Image[] thumbnails;


    void Start()
    {
        var numArtists = Artworks.Instance.numOnDisplay;
        for (int i = 0; i < numArtists; i++)
        {
            images[i].sprite = FetchedImages.Instance.images[i];
            thumbnails[i].sprite = FetchedImages.Instance.thumbnails[i];
            /*
            if (images[i].sprite == null) // 画像が取得できなかった場合，ローカルのものと差し替える.
            {
                var fileName = i < 10 ? "0" + i.ToString() : i.ToString();
                imgSprites[i] = Resources.Load<Sprite>("images/" + fileName);
            }
            */
        }
    }
}
