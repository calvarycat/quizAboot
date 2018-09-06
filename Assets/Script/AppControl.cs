using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class AppControl : MonoBehaviour
{
    public static AppControl instance;
    public SentResult sentResult;
    public Result listResult;
    // Use this for initialization
    public void Awake()
    {
        instance = this;      
        GetFakeData();
        //SendData();
    }

    public void SendData()
    {
        StartCoroutine(Upload());

    }
    public void GetFakeData()
    {
        sentResult = new SentResult();
        sentResult.deviceID = SystemInfo.deviceUniqueIdentifier;
        sentResult.trinhDuocVienName = "Nguyen Ngoc Trung";
        sentResult.tinhThanhTrinhDuocVien = "DakLak";


        List<Result> rs = new List<Result>();

        Result a = new Result();
        a.isDoctor = true;
        a.hovaTen = "nguyen Van A";
        a.khoa = "kinh mach";
        a.BenhVien = "Chợ rẫy";
        a.dateFinishTest = DateTime.Now.ToString();
        a.timePlay = 105.5f;
        rs.Add(a);

        Result b = new Result();
        b.isDoctor = false;
        b.hovaTen = "nguyen Van B";
        b.codeNhaThuoc = "21535452";
        b.TenNhaThuoc = "Long châu";
        b.tinhThanhRep = "An Giang";
        b.dateFinishTest = DateTime.Now.ToString();
        b.timePlay = 105.5f;
        rs.Add(b);

        sentResult.listResults.AddRange(rs);
    }

    IEnumerator Upload()
    {
        string rs = JsonMapper.ToJson(sentResult);
      //  Debug.Log(rs);
        if (!string.IsNullOrEmpty(rs))
        {
            WWWForm form = new WWWForm();
            form.AddField("data", rs);

            UnityWebRequest www = UnityWebRequest.Post("http://www.my-server.com/myform", form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
        else
        {
            Debug.Log("No data to upload!");
        }

    }
}
