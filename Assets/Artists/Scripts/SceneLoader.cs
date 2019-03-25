using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Scene scene;
    public PageManager pageManager;
    private float elapsedTime;
    public float messageShowingTime = 10f;
    public float inputWaitingTime = 60f;


    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (scene.name == "0_Title" || scene.name == "1_Main")
            return;

        CountDownToGoHome();
        if (scene.name == "2_Purchase")
        {
            if (Input.GetMouseButton(0) || Input.anyKey)
                elapsedTime = 0;
        }
    }

    private void CountDownToGoHome()
    {
        elapsedTime += Time.deltaTime;

        var timeLimit = scene.name == "2_Purchase" ? inputWaitingTime : messageShowingTime;
        if (elapsedTime > timeLimit)
        {
            Artworks.Instance.selectedWorkID = 0;
            //SelectedArtwork.Instance.name = "";
            LoadMainScene();
        }
    }

    public void OnClick()
    {
        switch (scene.name)
        {
            case "1_Main": // 閲覧画面から購入画面へ移動.
                Artworks.Instance.selectedWorkID = pageManager.currentPage;
                //SelectedArtwork.Instance.name = artistName.text;
                SceneManager.LoadScene("2_Purchase");
                break;
            case "2_Purchase": // 購入画面から閲覧画面へ戻る.
                LoadMainScene();
                break;
            case "3_Sent": // 送信完了画面から閲覧画面へ戻る.
                Artworks.Instance.selectedWorkID = 0;
                //SelectedArtwork.Instance.name = "";
                LoadMainScene();
                break;
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("1_Main");
    }

    public void LoadSentScene()
    {
        SceneManager.LoadScene("3_Sent");
    }
}


public class Artworks
{
    public readonly static Artworks Instance = new Artworks();
    // ここで設定するのは作品数の上限（ex.最大数が100なら，展示作品数が80であっても000.jpg - 100.jpgを取りに行く）にして，
    // 実際に表示するページ数はPageManagerに管理させるべき. その場合，展示作品数をどのように取得するか？
    public int numArtworks = 20;
    public int selectedWorkID = 1;
}

