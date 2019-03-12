using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSetter : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[Artworks.Instance.numArtists];
    public Image[] images;


    void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = FetchedImages.Instance.images[i];

            if (sprites[i] == null) // 画像が取得できなかった場合，ローカルのものと差し替える.
            {
                var fileName = i < 10 ? "0" + i.ToString() : i.ToString();
                sprites[i] = Resources.Load<Sprite>("images/" + fileName);
            }
        }
        SetImages();
    }

    void SetImages()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].sprite = sprites[i];
        }
    }
}
