using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    private Rect sourceImageSize = new Rect(0, 0, 1920, 1200);
    private int numArtists;
    private int id;
    public string[] imageURLs;
    private int numDone;

    public Text loadingText;
    public SceneLoader sceneLoader;


    private void Start()
    {
        numArtists = Artworks.Instance.numArtworks;

        foreach (var item in FetchedImages.Instance.images)
        {
            StartCoroutine("GetTexture", id);
            id++;
        }
    }

    private void Update()
    {
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        if (numDone >= numArtists * 2)
            sceneLoader.LoadMainScene();
    }

    /// <summary>
    /// 各ページの画像をロードする.
    /// </summary>
    IEnumerator GetTexture(int pageID)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURLs[id]);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            FetchedImages.Instance.images[pageID] = Sprite.Create(myTexture, sourceImageSize, Vector2.zero);
        }
        numDone++;
    }
}

public class FetchedImages
{
    public readonly static FetchedImages Instance = new FetchedImages();
    public Sprite[] images = new Sprite[Artworks.Instance.numArtworks];
    public Sprite[] thumbnails = new Sprite[Artworks.Instance.numArtworks];
}