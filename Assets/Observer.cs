using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public LoadImage loader;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Data.Instance.isComplete = loader.isComplete;
    }
}

public class Data
{
    public readonly static Data Instance = new Data();
    public bool isComplete;
}
