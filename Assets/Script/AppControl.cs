using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AppControl : MonoBehaviour
{
    public string url = "abbott-app.ssd-asia.com/api/player/play";
    public Text txtTest;

    public static AppControl instance;
    public PNInfor panelInfor;
    public DataSync data;

    //public SentResult sentResult;
    //public Result listResult;
    public int round = 1;
    public float timeplay = 0;
    // Use this for initialization

    UserInfo usserSessionInfor = new UserInfo();
    public GameState gamestate = 0;

    public void Awake()
    {
        instance = this;
        round = 1;
        timeplay = 0;

    }
    private void Start()
    {
        // SendData();
    }

    public void ResetData()
    {
        data.player = new List<UserInfo>();
    }
    public void StartNewSession()
    {
        Debug.Log("start new session");
        usserSessionInfor = new UserInfo();
        panelInfor.ResetData();
        round = 1;
        timeplay = 0;

    }
    public void SendData()
    {
        GetOwner();
        GetSave();
        GetUserPlay();
        StartCoroutine(Upload());

    }
    public void GetOwner()
    {

        data.device.client_name = SystemInfo.deviceName;
        data.device.client_version = SystemInfo.deviceModel;
        data.device.platform_name = SystemInfo.operatingSystem;
        data.device.uid = SystemInfo.deviceUniqueIdentifier;
    }
    public void GetSave()
    {

    }
    public void GetUserPlay()
    {

        usserSessionInfor.isDoctor = panelInfor.isDoctor;


        usserSessionInfor.timeplay = 22;
        if (usserSessionInfor.isDoctor == true)
        {
            usserSessionInfor.fullname = panelInfor.txtHotenBacsi.text;
            usserSessionInfor.hospital = "mat";
            usserSessionInfor.major = "IT";
        }
        else
        {
            usserSessionInfor.fullname = panelInfor.txtHotenDuocsi.text;

        }
        usserSessionInfor.timeplay = timeplay;
        data.player.Add(usserSessionInfor);

    }

    IEnumerator Upload()
    {
        yield return new WaitForEndOfFrame();
        BaseOnline.instance.SendData(data);
        //string st = txtTest.text;
        yield return null;
        // WWW w = new WWW(url,fo)
        //string rs = JsonMapper.ToJson(sentResult);
        ////  Debug.Log(rs);
        //if (!string.IsNullOrEmpty(rs))
        //{
        //    WWWForm form = new WWWForm();
        //    form.AddField("data", rs);

        //    UnityWebRequest www = UnityWebRequest.Post(url, form);
        //    yield return www.SendWebRequest();

        //    if (www.isNetworkError || www.isHttpError)
        //    {
        //        Debug.Log(www.error);
        //    }
        //    else
        //    {
        //        Debug.Log(www.ToString());
        //    }
        //}
        //else
        //{
        //    Debug.Log("No data to upload!");
        //}

    }
    public void FinishApp()
    {
        Application.LoadLevel(0);
    }


    public void OnResert()
    {
        panelInfor.OnShow(true);
        UIQuiz.instance.OnShow(false);
        QuizzDetail.instance.OnSHow(false);
        QuizzPart2.instance.OnShow(false);

    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FinishApp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            round = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            round = 2;
        }
        if (gamestate == GameState.Start)
        {
            timeplay += Time.deltaTime;
        }

    }
}
public enum GameState
{
    Stop = 0,
    Start = 1,
}