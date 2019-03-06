using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// Get the latest webcam shot from outside "Friday's" in Times Square
public class LoadImage : MonoBehaviour
{
    public string url;
    private Rect sourceImageSize = new Rect(0, 0, 800, 600);
    public Image[] image;


    IEnumerator Start()
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
            }

        }
    }
}