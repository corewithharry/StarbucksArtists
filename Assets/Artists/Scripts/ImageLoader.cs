using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    private Rect sourceImageSize = new Rect(0, 0, 800, 600);
    public Text loadingText;
    public SceneLoader sceneLoader;


    private void Start()
    {
        StartCoroutine("GetTexture");
    }

    private void Update()
    {
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    /// <summary>
    /// 各ページの画像をロードする.
    /// </summary>
    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://github.com/h-nishihata/MediaArtProgram2014/blob/master/ProcessingSample/P2_Images_1/data/photo_0.jpg?raw=true");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                FetchedImages.Instance.images[i] = Sprite.Create(myTexture, sourceImageSize, Vector2.zero);
            }
        }
        sceneLoader.LoadMainScene();
    }
}

public class FetchedImages
{
    public readonly static FetchedImages Instance = new FetchedImages();
    public Sprite[] images = new Sprite[20];
}