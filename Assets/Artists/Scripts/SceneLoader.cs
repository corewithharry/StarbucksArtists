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
            LoadMainScene();
        }
    }

    public void OnClick()
    {
        switch (scene.name)
        {
            case "1_Main": // 閲覧画面から購入画面へ移動.
                Artworks.Instance.selectedWorkID = pageManager.currentPage;
                SceneManager.LoadScene("2_Purchase");
                break;
            case "2_Purchase": // 購入画面から閲覧画面へ戻る.
                LoadMainScene();
                break;
            case "3_Sent": // 送信完了画面からアーティスト一覧画面へ戻る.
                Artworks.Instance.selectedWorkID = 0;
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

/// <summary>
/// 作品画像番号を管理するクラス.
/// </summary>
public class Artworks
{
    public static readonly Artworks Instance = new Artworks();
    // アプリが扱う作品数の上限. 40なら，展示作品数が80であっても起動時に最新の00.jpg - 40.jpgを取りに行く.
    // 40以上になるとどう頑張ってもエラーになるので，小さい作品は１ページに纏めるなど，運用面でカバーしてもらう（190328現在）.
    // 更新された画像のみをCSVで管理し取得する方法も考えたが，全ページを一斉更新したり，立て続けに更新されたりすると反映が追いつかないので断念. orz
    public int numMaxArtworks = 40;
    // CSVファイルを読み込んで設定される展示作品数. この値にもとづいて画像とページの数を決めている.
    public int numOnDisplay;
    // 選択中の作品ID.
    public int selectedWorkID = 1;
    public List<string[]> csvData;
}

