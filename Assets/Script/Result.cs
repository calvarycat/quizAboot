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