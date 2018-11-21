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
        GetSave();

        round = 1;
        timeplay = 0;
        GetOwner();
    }


    public void GetSave()
    {
        string value = "";
        value= PlayerPrefs.GetString("datasave", "");
        if(!string.IsNullOrEmpty(value))
        {
            data = JsonMapper.ToObject<DataSync>(value);
        }
        else
        {
            data = new DataSync();
            GetOwner();
        }
     

    }
    public void SaveData()
    {
        string dt = JsonMapper.ToJson(data);
        PlayerPrefs.SetString("datasave", dt);
    }

  

    public void ResetData()
    {
        data.player = new List<UserInfo>();
        SaveData();
    }
    public void StartNewSession()
    {
        AudioManager.instance.PlaySoundGame1();
        usserSessionInfor = new UserInfo();
        panelInfor.ResetData();
        panelInfor.OnShow(true);
        UIQuiz.instance.OnShow(false);
        QuizzDetail.instance.OnSHow(false);
        QuizzPart2.instance.OnShow(false);
       
         round = 1;
        timeplay = 0;

    }
    public void SendData()
    {
      
        GetSave();
        GetUserPlay();
        PlayerPrefs.GetString("datasave", "");
        StartCoroutine(Upload(OnSendFinish));

    }
    public void SynData( Action SendFinish)
    {       
            StartCoroutine(Upload(SendFinish));
    }
    public void OnSendFinish()
    {
      //  Debug.Log("send finish  app control");
    }
    public void GetOwner()
    {

        data.device.client_name = SystemInfo.deviceName;
        data.device.client_version = SystemInfo.deviceModel;
        data.device.platform_name = SystemInfo.operatingSystem;
        data.device.uid = SystemInfo.deviceUniqueIdentifier;
    }
 
    public void GetUserPlay()
    {

        usserSessionInfor.isDoctor = panelInfor.isDoctor;
        usserSessionInfor.timeplay = 22;
        if (usserSessionInfor.isDoctor == true)
        {
            usserSessionInfor.fullname = panelInfor.txtHotenBacsi.text;
            usserSessionInfor.hospital = panelInfor.txtBenhVien.text;
            usserSessionInfor.major = panelInfor.txtKhoa.text;
            usserSessionInfor.place = "";
            usserSessionInfor.codeStore = "";
            usserSessionInfor.storeName = "";
        }
        else
        {
            usserSessionInfor.fullname = panelInfor.txtHotenDuocsi.text;
          
            usserSessionInfor.codeStore = panelInfor.txtCodeNhathuoc.text;
            usserSessionInfor.storeName = panelInfor.txtTenNhathuoc.text;
          //  usserSessionInfor.place = Provices.instance.rootProvices.provices[panelInfor.drpTinhThanhDuocsi.value].TenTinhThanh;

        }
        usserSessionInfor.timeplay = timeplay;
        usserSessionInfor.date = DateTime.Today.ToString() ;
        data.player.Add(usserSessionInfor);
    }

    IEnumerator Upload( Action Onfinish)
    {
        yield return new WaitForEndOfFrame();
        BaseOnline.instance.SendData(data, Onfinish);
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
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    FinishApp();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    round = 1;
        //    UIQuiz.instance.OnShow(false);
        //    QuizzDetail.instance.OnSHow(false);
        //    StartNewSession();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    round = 2;
        //    QuizzPart2.instance.InitQuizz();
        //}
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