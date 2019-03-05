using UnityEngine;
using System.Collections.Generic;

public class Permutation : MonoBehaviour
{
    private List<string> list;
    private List<string> tempResult;
    private List<string> result;

    private int numParicles;
    int lastNumber0;
    int lastNumber1;

    private Camera mainCamera;
    private CSVWriter csvWriter;

    int iterCount;

    public void OnValueChanged(int dromDownValue)
    {
        numParicles = dromDownValue;
    }

    public void ManualStart()
    {
        list = new List<string>() { "2", "3"/*, "4", "5", "6", "7"*/};
        tempResult = new List<string>();
        result = new List<string>();

        mainCamera = Camera.main;
        csvWriter = mainCamera.GetComponent<CSVWriter>();

        GeneratePatterns();
    }

    public void GeneratePatterns()
    {
        for (int i = 0; i < list.Count; i++)
        {
            tempResult.Add(list[i]);
            lastNumber0 = i;

            for (int j = 0; j < list.Count; j++)
            {
                //if (j == lastNumber0) continue;
                tempResult.Add(list[j]);
                lastNumber1 = j;

                if (iterCount < 1)
                {
                    iterCount = 1;
                    Debug.Log("<color=red>iterCount: </color>" + iterCount);
                    GeneratePatterns();
                }
                //for (int k = 0; k < count; k++)
                //{
                //    if (k == lastNumber1) continue;
                //    tempResult.Add(list[k]);
                //if(tempResult.Count == 4)
                    Save(tempResult);
                //    tempResult.RemoveAt(tempResult.Count - 1);
                //}

                tempResult.RemoveAt(tempResult.Count - 1);
            }
            iterCount++;
            Debug.Log("<color=red>iterCount: </color>" + iterCount);

            tempResult.RemoveAt(tempResult.Count - 1);
            if (iterCount == 3)
            {
                iterCount = 0;
            }
            //if (iterCount > list.Count * 2)
            //{
            //    tempResult.Clear();
            //    iterCount = 0;
            //    Debug.Log("<color=red>iterCount: </color>" + iterCount);
            //}
        }
    }

    public void Save(List<string> tempResult)
    {
        //result.Add("\n");
        result.Add("1");
        for (int i = 0; i < tempResult.Count; i++)
        {
            result.Add(tempResult[i]);
        }
        result.Add("8");

        var temp = result.ToArray();
        Debug.Log(string.Join(",", temp));
        result.Clear();

        //stringArray = new string[] { "\n", "1,2,3", "4,5,6", "7,8,9" };
        //csvWriter.Save(stringArray, "patternData");
    }
}