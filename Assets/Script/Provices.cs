using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Provices : MonoBehaviour
{

    public static Provices instance;
    public RootProvices rootProvices;
    // Use this for initialization
    void Awake()
    {
        instance = this;
        LoadProvices("Jsonfile/Provices");

    }




    public void LoadProvices(string path)
    {
        try
        {
            TextAsset textAsset = Resources.Load<TextAsset>(path);
            string asset = textAsset.text;
            rootProvices = JsonMapper.ToObject<RootProvices>(asset);
        }
        catch (Exception error)
        {
            Debug.LogError("Getting question Data error: " + error.StackTrace);
        }
    }
}
[System.Serializable]
public class Provice
{
    public string Ma;
    public string TenTinhThanh;
}
[System.Serializable]
public class RootProvices
{
    public List<Provice> provices;
}