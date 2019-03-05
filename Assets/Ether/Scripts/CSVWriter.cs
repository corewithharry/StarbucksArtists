using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour
{
    public void Save(string[] data, string fileName)
    {
        StreamWriter sw;
        FileInfo fi;

        fi = new FileInfo(Application.dataPath + "/Resources/" + fileName + ".csv");
        sw = fi.AppendText();
        for (int i = 0; i < data.Length; i++)
        {
            sw.WriteLine(data[i]);
        }

        sw.Flush();
        sw.Close();
    }
}
