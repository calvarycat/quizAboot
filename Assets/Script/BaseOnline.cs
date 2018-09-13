using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using LitJson;
using UnityEngine.UI;

public class BaseOnline : MonoBehaviour
{
    public static BaseOnline instance;
    public float timeout = 20;

    public delegate void FinishPost(bool success);
    public FinishPost OnFinishSend;
    public Hashtable headers;    
    private readonly string url = "http://abbott-app.ssd-asia.com/api/player/play";
    public WWW www;
  //  public Scrollbar srb;
    public void Awake()
    {
        instance = this;
    }

    private IEnumerator Post(DataSync data,Action SendFinish)
    {
        AppControl.instance.SaveData();
        bool haveData = false;
        float timeset = 0;
        data.device.token = "qQH7Vg6tpk";
       Debug.Log("<color=blue>" +url +"</color> "+ JsonMapper.ToJson(data));
        byte[] dataSend = Encoding.UTF8.GetBytes(JsonMapper.ToJson(data));
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");
        www = new WWW(url, dataSend, headers);
        while (timeset <= timeout)
        {
            timeset++;
            if (!www.isDone)
            {
              
                //if(sr)
                //{
                // //   srb.value = www.progress;
                //}
                Debug.Log(www.progress);
                yield return new WaitForSeconds(.1f);
            }
            else
            {
                haveData = true;
                // break;
            }
        }
       
        //yield return www;
        //Debug.Log("data response " + www.text);
        if (haveData)
        {
            try
            {
                Debug.Log("data response : "+www.text);
                ServerMessage serverRes = JsonMapper.ToObject<ServerMessage>(www.text);
                SendFinish();
                if (serverRes.status.Contains("success"))
                {
                    Debug.Log("send success");                  
                    AppControl.instance.ResetData(); // resert data player sau khi choi
                    // write vo log??
                    if (OnFinishSend != null)
                        OnFinishSend(true);
                 
                }
                //else if (serverRes.status.Contains("rewardDoctor"))
                //{
                //    PlayerPrefs.SetInt("Reward", 1);
                //    OnFinishSend(true);
                //    PlayerPrefs.Save();
                //}
                //else if (serverRes.status.Contains("rewardPharmacy"))
                //{
                //    PlayerPrefs.SetInt("Reward", 2);
                //    OnFinishSend(true);
                //    PlayerPrefs.Save();
                //}
                //else if (serverRes.status.Contains("block"))
                //{
                //    PlayerPrefs.SetInt("block", 1);
                //    PlayerPrefs.Save();
                //}
                //else
                //{
                //    OnFinishSend(false);
                //}
                data.device.token = "";
            }
            catch (Exception err)
            {
                SendFinish();
                Debug.Log("Updata Error: " + err);
                if(OnFinishSend!=null)
                OnFinishSend(false);
            }
        }
        else
        {
            SendFinish();
            Debug.Log("have no data");
            if (OnFinishSend != null)
                OnFinishSend(false);
        }
    }

    //private IEnumerator ShowProgress(WWW www)
    //{
    //    while (!www.isDone)
    //    {
    //        Debug.Log(string.Format("Downloaded {0:P1}", www.progress));
    //        yield return new WaitForSeconds(.1f);
    //    }
    //    Debug.Log("Done");
    //}

    public void SendData(DataSync data,Action SendFinish)
    {     
      
        StartCoroutine(Post(data, SendFinish));
    }
}

[Serializable]
public class DataSync
{
    public DeviceInfo device = new DeviceInfo();
    public List<UserInfo> player = new List<UserInfo>();
}

[Serializable]
public class DeviceInfo
{
    public string client_name;
    public string client_version;
    public string platform_name;
    public string platform_version = "1.0.0";
    public string uid;
    public string token;
    public string owner = "";
    public string branch = "";
}


public class ServerMessage
{
    public string status;
}