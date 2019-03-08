using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public ImageLoader imageLoader;
    public Text artistName;
    public Text loadingText;
    private bool isLoading;
    private float elapsedTime;
    private float waitingTime = 10f;


    private void Start()
    {
        var scene = SceneManager.GetActiveScene();
        if (scene.name == "0_Title")
        {
            StartCoroutine("ShowTitleScene");
            isLoading = true;
        }
    }

    private void Update()
    {
        if(isLoading)
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }

        var scene = SceneManager.GetActiveScene();
        if (scene.name == "3_Thanks")
            countDownToGoHome();
    }

    private void countDownToGoHome()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > waitingTime)
        {
            SelectedArtwork.Instance.id = 0;
            SelectedArtwork.Instance.name = "";
            SceneManager.LoadScene("1_Main");
        }
    }

    public void OnClick()
    {
        var scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "1_Main": // 閲覧画面から購入画面へ移動.
                SelectedArtwork.Instance.id = imageLoader.pageID;
                SelectedArtwork.Instance.name = artistName.text;
                SceneManager.LoadScene("2_Purchase");
                break;
            case "2_Purchase": // 購入画面から閲覧画面へ戻る.
                SceneManager.LoadScene("1_Main");
                break;
            case "3_Thanks": // 送信完了画面から閲覧画面へ戻る.
                SelectedArtwork.Instance.id = 0;
                SelectedArtwork.Instance.name = "";
                SceneManager.LoadScene("1_Main");
                break;
        }
    }

   　private IEnumerator ShowTitleScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("1_Main");
    }
}


public class SelectedArtwork
{
    public readonly static SelectedArtwork Instance = new SelectedArtwork();
    public int id = 1;
    public string name;
}

