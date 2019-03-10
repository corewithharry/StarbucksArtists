using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Scene scene;
    public SlideShow slideShow;
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
            case "3_Sent": // 送信完了画面から閲覧画面へ戻る.
                SelectedArtwork.Instance.id = 0;
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


public class SelectedArtwork
{
    public readonly static SelectedArtwork Instance = new SelectedArtwork();
    public int id = 1;
    //public string name;
}

