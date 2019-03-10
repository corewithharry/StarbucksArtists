using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Scene scene;
    private float elapsedTime;
    private float waitingTime = 10f;
    public SlideShow slideShow;


    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("1_Main");
    }

    private void Update()
    {
        if (scene.name == "3_Thanks")
            countDownToGoHome();
    }

    private void countDownToGoHome()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > waitingTime)
        {
            SelectedArtwork.Instance.id = 0;
            //SelectedArtwork.Instance.name = "";
            LoadMainScene();
        }
    }

    public void OnClick()
    {
        switch (scene.name)
        {
            case "1_Main": // 閲覧画面から購入画面へ移動.
                SelectedArtwork.Instance.id = slideShow.currentPage;
                //SelectedArtwork.Instance.name = artistName.text;
                SceneManager.LoadScene("2_Purchase");
                break;
            case "2_Purchase": // 購入画面から閲覧画面へ戻る.
                LoadMainScene();
                break;
            case "3_Thanks": // 送信完了画面から閲覧画面へ戻る.
                SelectedArtwork.Instance.id = 0;
                //SelectedArtwork.Instance.name = "";
                LoadMainScene();
                break;
        }
    }
}


public class SelectedArtwork
{
    public readonly static SelectedArtwork Instance = new SelectedArtwork();
    public int id = 1;
    //public string name;
}

