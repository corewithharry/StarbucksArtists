using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    private Rect sourceImageSize = new Rect(0, 0, 1184, 1113);
    private Rect sourceThumbnailSize = new Rect(0, 0, 380, 380);
    private int numArtists;
    private int id;
    public string[] imageURLs;
    public string[] thumbnailURLs;
    private int doneLoadImg;
    private int doneLoadThumbs;

    public Text loadingText;
    public SceneLoader sceneLoader;


    private void Start()
    {
        numArtists = Artworks.Instance.numArtworks;

        foreach (var item in FetchedImages.Instance.images)
        {
            StartCoroutine("LoadImages", id);
            id++;
        }
        foreach (var item in FetchedImages.Instance.thumbnails)
        {
            id = 0;
            StartCoroutine("LoadThumbnails", id);
            id++;
        }
    }

    private void Update()
    {
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));

        if ((doneLoadImg >= numArtists) && (doneLoadThumbs >= numArtists))
            sceneLoader.LoadMainScene();
    }

    /// <summary>
    /// 各ページの画像をロードする.
    /// </summary>
    IEnumerator LoadImages(int pageID)
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
        doneLoadImg++;
    }

    IEnumerator LoadThumbnails(int pageID)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(thumbnailURLs[id]);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            FetchedImages.Instance.thumbnails[pageID] = Sprite.Create(myTexture, sourceThumbnailSize, Vector2.zero);
        }
        doneLoadThumbs++;
    }
}

public class FetchedImages
{
    public readonly static FetchedImages Instance = new FetchedImages();
    public Sprite[] images = new Sprite[Artworks.Instance.numArtworks];
    public Sprite[] thumbnails = new Sprite[Artworks.Instance.numArtworks];
}