using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShowArtistName : MonoBehaviour
{
    private TextAsset csvFile;
    public List<string[]> csvData = new List<string[]>();
    private StringBuilder selectedArtwork = new StringBuilder();
    public Text artistName;


    void Awake()
    {
        csvFile = Resources.Load<TextAsset>("artworkInfo");
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvData.Add(line.Split(','));
        }
    }

    /// <summary>
    /// 購入画面で，選択した作品と作家の名前を表示する.
    /// </summary>
    void Start()
    {
        selectedArtwork.Clear();
        var id = Artworks.Instance.selectedWorkID;
        selectedArtwork.Append(id + ")　"); // 作品番号.
        if (csvData[id] != null)
        {
            selectedArtwork.Append(csvData[id][0] + "　"); // 作家名.
            selectedArtwork.Append("「" + csvData[id][1] + "」"); // 作品名.
        }
        artistName.text = selectedArtwork.ToString();
    }


}
