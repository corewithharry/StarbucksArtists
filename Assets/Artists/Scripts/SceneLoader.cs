using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Text artistName;
    public Text loadingText;
    private bool isLoading;

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
    }

    public void OnClick()
    {
        var scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "1_Main":
                Artwork.Instance.name = artistName.text;
                SceneManager.LoadScene("2_Purchase");
                break;
            case "2_Purchase":
                SceneManager.LoadScene("1_Main");
                break;
            case "3_Thanks":
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
