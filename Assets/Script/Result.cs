using System.Collections.Generic;

[System.Serializable]
public class Result {  
    public bool isDoctor;
    public string hovaTen;
    public string khoa;
    public string BenhVien;
    public string codeNhaThuoc;
    public string TenNhaThuoc;
    public string tinhThanhRep;
    public string dateFinishTest;
    public float timePlay;
}
public class SentResult
{
    public string deviceID;
    public string trinhDuocVienName;
    public string tinhThanhTrinhDuocVien;
    public List<Result> listResults = new List<Result>();
}



public class Device
{
    public string client_name { get; set; }
    public string client_version { get; set; }
    public string platform_name { get; set; }
    public string platform_version { get; set; }
    public string uid { get; set; }
    public string token { get; set; }
    public string owner { get; set; }
    public string branch { get; set; }
}

public class Player
{
    public string fullname { get; set; }
    public string hospital { get; set; }    
    public string major { get; set; }

    public string codeStore;
    public string storeName;
    public string place { get; set; }
    public float timeplay { get; set; }
    public string date { get; set; }
    public bool isDoctor { get; set; }
}

public class RootObject
{
    public Device device { get; set; }
    public List<Player> player { get; set; }
}