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
