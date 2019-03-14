using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetter : MonoBehaviour
{
    public Sprite[] imgSprites = new Sprite[Artworks.Instance.numArtworks];
    public Sprite[] thumbnailSprites = new Sprite[Artworks.Instance.numArtworks];
    public Image[] images;
    public Image[] thumbnails;


    void Start()
    {
        for (int i = 0; i < imgSprites.Length; i++)
        {
            imgSprites[i] = FetchedImages.Instance.images[i];

            if (imgSprites[i] == null) // 画像が取得できなかった場合，ローカルのものと差し替える.
            {
                var fileName = i < 10 ? "0" + i.ToString() : i.ToString();
                imgSprites[i] = Resources.Load<Sprite>("images/" + fileName);
            }
        }

        for (int i = 0; i < thumbnailSprites.Length; i++)
        {
            thumbnailSprites[i] = FetchedImages.Instance.thumbnails[i];
            if (thumbnailSprites[i] == null)
            {
                var fileName = i < 10 ? "0" + i.ToString() : i.ToString();
                thumbnailSprites[i] = Resources.Load<Sprite>("thumbnails/" + fileName);
            }
        }
        SetImages();
    }

    void SetImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = imgSprites[i];
        }
        for (int i = 0; i < thumbnails.Length; i++)
        {
            thumbnails[i].sprite = thumbnailSprites[i];
        }
    }
}
