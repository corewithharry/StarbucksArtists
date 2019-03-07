using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    public Text artistName;


    public void OnClick()
    {
        Artwork.Instance.name = artistName.text;
        SceneManager.LoadScene("Purchase");
    }
}
