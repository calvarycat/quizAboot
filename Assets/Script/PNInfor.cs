using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNInfor : MonoBehaviour
{
    public GameObject root;
    public Dropdown drpProvices;
    int idProvicesAM;
    public Dropdown drpTinhThanhDuocsi;
    int idProvicesDuocsi;
    public InputField txtHotenAm;
    public GameObject RootRightAM, RightMoiBacsiDuocsi;

    bool isBacsi = true;
    public GameObject BacSi, DuocSi;
    public InputField txtHotenBacsi, txtKhoa, txtBenhVien;
    public InputField txtHotenDuocsi, txtCodeNhathuoc, txtTenNhathuoc;

    public GameObject pnChangeInfor;
    public Text txtNameChangeInfor;

    // Use this for initialization
    void Start()
    {
        FillDropdown(drpProvices, Provices.instance.rootProvices.provices);
        FillDropdown(drpTinhThanhDuocsi, Provices.instance.rootProvices.provices);
        LoadData();
    }
    public void LoadData()
    {
        // load data 
        // txtHotenAm.text = AppControl.instance.listResult.trinhDuocVienName;
        drpProvices.value = 1;
    }
 
    public void OnShow(bool isShow)
    {
        root.SetActive(isShow);
    }
    public void OnResert()
    {
        Debug.Log("reset");
    }

    public void FillDropdown(Dropdown dr, List<Provice> region)
    {
        dr.options.Clear();
        foreach (Provice str in region)
        {
            dr.options.Add(new Dropdown.OptionData(str.TenTinhThanh));

        }
        dr.captionText.text = "Select a region";

    }
    public void OnDropDownClick(int id)
    {
        idProvicesAM = id;
     //   Debug.Log("Choose This ID" + id + Provices.instance.rootProvices.provices[id].TenTinhThanh);
    }


    public void OnDangNhapAM()
    {
        AudioManager.instance.PlayButtonClick();
        if (CheckAMInfor())
        {
            PopUpmanager.instance.InitInfor("Check information",null);
            return;
        }
        AppControl.instance.sentResult.trinhDuocVienName = txtHotenAm.text;
        AppControl.instance.sentResult.tinhThanhTrinhDuocVien = Provices.instance.rootProvices.provices[idProvicesAM].TenTinhThanh;
        RootRightAM.SetActive(false);
        RightMoiBacsiDuocsi.SetActive(true);
    }
    public bool CheckAMInfor()
    {
        if (string.IsNullOrEmpty(txtHotenAm.text))
        {
            return true;
        }
        return false;
    }

    #region Tham gia thu thach

    public void OnBacSiClick()
    {
        isBacsi = true;
        BacSi.SetActive(true);
        DuocSi.SetActive(false);
        AudioManager.instance.PlayButtonClick();
    }
    public void OnDuocSiClick()
    {
        isBacsi = false;
        BacSi.SetActive(false);
        DuocSi.SetActive(true);
        AudioManager.instance.PlayButtonClick();
    }
    


    public void OnDangNhapStartGame()
    {
        AudioManager.instance.PlayButtonClick();
        // check thong tin khac null dax
        if (isBacsi)
        {
            if (CheckThongTinBacSi())
            {
                PopUpmanager.instance.InitInfor("Check information", null);
                return;
            }
        }else
        {
            if (CheckThongtinDuocsi())
            {
                PopUpmanager.instance.InitInfor("Check information", null);
                return;
            }

        }
       
        AppControl.instance.sentResult.tinhThanhTrinhDuocVien = Provices.instance.rootProvices.provices[idProvicesAM].TenTinhThanh;
        UIQuiz.instance.OnShow(true);
        OnShow(false);
    }
    public bool CheckThongTinBacSi()
    {
        if (string.IsNullOrEmpty(txtHotenBacsi.text)|| string.IsNullOrEmpty(txtKhoa.text)|| string.IsNullOrEmpty(txtBenhVien.text))
        {
            return true;
        }
        return false;
    }
    public bool CheckThongtinDuocsi()
    {
        if (string.IsNullOrEmpty(txtHotenDuocsi.text)|| string.IsNullOrEmpty(txtCodeNhathuoc.text)|| string.IsNullOrEmpty(txtTenNhathuoc.text))
        {
            return true;
        }
        return false;
    }
    public void OnDropdownDuocsiClick(int id)
    {
        idProvicesDuocsi = id;
        Debug.Log("Choose This ID" + id + Provices.instance.rootProvices.provices[id].TenTinhThanh);
    }

    public void OnInforClick()
    {
        AudioManager.instance.PlayButtonClick();
        txtNameChangeInfor.text = AppControl.instance.sentResult.trinhDuocVienName;
        pnChangeInfor.SetActive(true);
    }
    public void OnChangeInforClick()
    {
        pnChangeInfor.SetActive(false);
        RootRightAM.SetActive(true);
        RightMoiBacsiDuocsi.SetActive(false);
    }
    public void OnSyndataClick()
    {

    }
    #endregion
}
