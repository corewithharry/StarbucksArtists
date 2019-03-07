using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ShowArtistName : MonoBehaviour
{
    private StringBuilder selectedArtwork = new StringBuilder();
    public Text artistName;


    void Start()
    {
        selectedArtwork.Clear();
        selectedArtwork.Append("選択した作品 / Selected artwork：　" + "\n");
        selectedArtwork.Append(Artwork.Instance.name);
        artistName.text = selectedArtwork.ToString();
    }
}
