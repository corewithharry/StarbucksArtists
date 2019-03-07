using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Text artistName;


    public void OnClick()
    {
        var scene = SceneManager.GetActiveScene();
        switch (scene.name)
        {
            case "Main":
                Artwork.Instance.name = artistName.text;
                SceneManager.LoadScene("Purchase");
                break;
            case "Thanks":
                SceneManager.LoadScene("Main");
                break;
        }
    }
}
