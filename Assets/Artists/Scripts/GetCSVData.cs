using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetCSVData : MonoBehaviour
{
    public string csvURL = "https://raw.githubusercontent.com/h-nishihata/BAL_Images/gh-pages/artworkInfo.csv";
    private TextAsset csvFile;
    public List<string[]> csvData = new List<string[]>();
    private string temp;

    private StringBuilder selectedArtwork = new StringBuilder();
    public Text artistName;
    private int numOnDisplay;


    void Awake()
    {
        var scene = SceneManager.GetActiveScene();
        if (scene.name == "0_Title")
        {
            StartCoroutine(LoadCSV());
            return;        
        }

        // 購入画面で，選択した作品と作家の名前を表示する.
        selectedArtwork.Clear();
        var id = Artworks.Instance.selectedWorkID;

        selectedArtwork.Append(id + ")　"); // 作品番号.
        if (Artworks.Instance.csvData == null)
            return;

        selectedArtwork.Append(Artworks.Instance.csvData[id][0] + "　"); // 作家名.
        selectedArtwork.Append("「" + Artworks.Instance.csvData[id][1] + "」"); // 作品名.

        artistName.text = selectedArtwork.ToString();
    }

    private IEnumerator LoadCSV()
    {
        UnityWebRequest www = UnityWebRequest.Get(csvURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            temp = www.downloadHandler.text;
            StringReader reader = new StringReader(temp);

            while (reader.Peek() > -1)
            {
                string line = reader.ReadLine();
                csvData.Add(line.Split(','));
                numOnDisplay++;
            }
            Artworks.Instance.csvData = csvData;
            Artworks.Instance.numOnDisplay = numOnDisplay - 1; // 展示中の作品数.
        }
    }
}