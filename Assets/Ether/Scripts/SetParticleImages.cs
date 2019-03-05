using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class SetParticleImages : MonoBehaviour
{
    private CSVReader csvReader;
    public int pageID;

    public GameObject[] particles;
    public int numMaxBoxes = 6;
    private int numBoxes;
    private int initLine;
    public Image[] images;
    public Color fillColor;

    public Text number;
    private StringBuilder lotNumber = new StringBuilder();

    public void WarmUp()
    {
        csvReader = transform.parent.GetComponent<CSVReader>();
        images = new Image[particles.Length];
        for (int i = 0; i < particles.Length; i++)
        {
            images[i] = particles[i].GetComponent<Image>();
        }
    }

    public void Trigger()
    {
        numBoxes = csvReader.numBoxes;
        initLine = csvReader.csvInitLine;

        lotNumber.Clear();
        SetImages();
    }

    private void SetImages()
    {
        for (int i = 0; i < numMaxBoxes; i++)
        {
            if (i < numBoxes)
            {
                images[i].color = Color.white;
                var numImages = csvReader.csvData[initLine + pageID][i];
                lotNumber.Append(numImages);
                var imageID = Int32.Parse(numImages);
                images[i].sprite = csvReader.sourceImages[imageID - 1];
            }
            else if(i >= numBoxes)
            {
                images[i].color = fillColor;
            }
        }

        number.text = lotNumber.ToString();
    }
}
