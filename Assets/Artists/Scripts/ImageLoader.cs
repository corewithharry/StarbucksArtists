#define USE_LOADING_IMAGES
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


/// <summary>
/// Imposes a limit on the maximum number of coroutines that can be running at any given time. Runs
/// coroutines until the limit is reached and then begins queueing coroutines instead. When
/// coroutines finish, queued coroutines are run.
/// </summary>
/// <author>Jackson Dunstan, http://JacksonDunstan.com/articles/3241</author>
public class CoroutineQueue
{
    /// <summary>
    /// Maximum number of coroutines to run at once
    /// </summary>
    private readonly uint maxActive;
 
    /// <summary>
    /// Delegate to start coroutines with
    /// </summary>
    private readonly Func<IEnumerator,Coroutine> coroutineStarter;
 
    /// <summary>
    /// Queue of coroutines waiting to start
    /// </summary>
    private readonly Queue<IEnumerator> queue;
 
    /// <summary>
    /// Number of currently active coroutines
    /// </summary>
    private uint numActive;

    /// <summary>
    /// Create the queue, initially with no coroutines
    /// </summary>
    /// <param name="maxActive">
    /// Maximum number of coroutines to run at once. This must be at least one.
    /// </param>
    /// <param name="coroutineStarter">
    /// Delegate to start coroutines with. Normally you'd pass
    /// <see cref="MonoBehaviour.StartCoroutine"/> for this.
    /// </param>
    /// <exception cref="ArgumentException">
    /// If maxActive is zero.
    /// </exception>
    public CoroutineQueue(uint maxActive, Func<IEnumerator,Coroutine> coroutineStarter)
    {
        if (maxActive == 0)
        {
            throw new ArgumentException("Must be at least one", "maxActive");
        }
        this.maxActive = maxActive;
        this.coroutineStarter = coroutineStarter;
        queue = new Queue<IEnumerator>();
    }
 
    /// <summary>
    /// If the number of active coroutines is under the limit specified in the constructor, run the
    /// given coroutine. Otherwise, queue it to be run when other coroutines finish.
    /// </summary>
    /// <param name="coroutine">Coroutine to run or queue</param>
    public void Run(IEnumerator coroutine)
    {
        if (numActive < maxActive)
        {
            var runner = CoroutineRunner(coroutine);
            coroutineStarter(runner);
        }
        else
        {
            queue.Enqueue(coroutine);
        }
    }
 
    /// <summary>
    /// Runs a coroutine then runs the next queued coroutine (via <see cref="Run"/>) if available.
    /// Increments <see cref="numActive"/> before running the coroutine and decrements it after.
    /// </summary>
    /// <returns>Values yielded by the given coroutine</returns>
    /// <param name="coroutine">Coroutine to run</param>
    private IEnumerator CoroutineRunner(IEnumerator coroutine)
    {
        numActive++;
        while (coroutine.MoveNext())
        {
            yield return coroutine.Current;
        }
        numActive--;
        if (queue.Count > 0)
        {
            var next = queue.Dequeue();
            Run(next);
        }
    }
}


public class ImageLoader : MonoBehaviour
{
    private Rect sourceImageSize = new Rect(0, 0, 1920, 1200);
    private Rect sourceThumbnailSize = new Rect(0, 0, 380, 380);
    private int numArtworks;
    public string[] imageURLs;
    public string[] thumbnailURLs;
    private int numLoadedImgs;
    private int numLoadedThumbs;

    public Text loadingText;
    public Text errorMessage;
    public Slider slider;
    public SceneLoader sceneLoader;


#if USE_LOADING_IMAGES
    void Start()
    {
        numArtworks = Artworks.Instance.numMaxArtworks;
        // Create a coroutine queue that can run up to two coroutines at once
        var queue = new CoroutineQueue(2, StartCoroutine);

        for(int i = 0; i < numArtworks; i++)
        {
            queue.Run(LoadImages(i));
            queue.Run(LoadThumbnails(i));
        }
    }

    private void Update()
    {
        loadingText.text = ("Loading images ...");
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        var progressVal = (float)numLoadedImgs / numArtworks;
        slider.value = progressVal;

        if ((numLoadedImgs >= numArtworks) && (numLoadedThumbs >= numArtworks))
            sceneLoader.LoadMainScene();
    }

    /// <summary>
    /// 各ページの画像とサムネイルをロードする.
    /// </summary>
    private IEnumerator LoadImages(int pageID)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURLs[pageID]);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            errorMessage.text = "Error: " + www.error;
        }
        else
        {
            Texture2D imgTex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            FetchedImages.Instance.images[pageID] = Sprite.Create(imgTex, sourceImageSize, Vector2.zero);
        }
        numLoadedImgs++;
    }

    private IEnumerator LoadThumbnails(int pageID)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(thumbnailURLs[pageID]);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            errorMessage.text = "Error: " + www.error;
        }
        else
        {
            Texture2D thumbTex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            FetchedImages.Instance.thumbnails[pageID] = Sprite.Create(thumbTex, sourceThumbnailSize, Vector2.zero);
        }
        numLoadedThumbs++;
    }

#else
    private void Update()
    {
        if (Input.GetMouseButton(0))
            sceneLoader.LoadMainScene();
    }
#endif
}


public class FetchedImages
{
    public readonly static FetchedImages Instance = new FetchedImages();
    public Sprite[] images = new Sprite[Artworks.Instance.numMaxArtworks];
    public Sprite[] thumbnails = new Sprite[Artworks.Instance.numMaxArtworks];
}