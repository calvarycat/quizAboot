using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PopUpmanager : MonoBehaviour
{
    public static PopUpmanager instance;
    public GameObject rootInfor;
    public Text txtContentÌnor;
    public Action _OnCloseInfor;

    public GameObject rootExplain;
    public Text txtcontentExplain, txtUnderline;
    public Action _OncloseExplain;

    public GameObject RootFinishRound1;
    public Text txtContentRound1;
    public Action _OncloseRound1;

    public GameObject RootConfirm;
    public Text txtConfrimText;
    public Action _OnYes;
    public Action _OnNo;

    public void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    public void InitInfor(string mes, Action onClose)
    {
        _OnCloseInfor = onClose;
        txtContentÌnor.text = mes;
        rootInfor.SetActive(true);

    }
    public void OnCloseInfor()
    {
        rootInfor.SetActive(false);
        if (_OnCloseInfor != null)
        {

            _OnCloseInfor();
        }
    }
    public void InitExplain(string mes, Action onClose)
    {
        timeOpen = 0;
        _OncloseExplain = onClose;
        txtcontentExplain.text = mes;

        rootExplain.SetActive(true);


    }
    float timeOpen = 0;
    public void Update()
    {

        timeOpen += Time.deltaTime;
        if (timeOpen >= 4)
        {
            if (rootExplain.activeSelf)
            {
                OnCloseExplain();
            }

        }
    }

    public void OnCloseExplain()
    {
        rootExplain.SetActive(false);

        if (_OncloseExplain != null)
        {
            _OncloseExplain();
        }
    }

    public void InitFinishRound1(string mes, Action onClose)
    {
        _OncloseRound1 = onClose;
        txtContentRound1.text = mes;
        RootFinishRound1.SetActive(true);

    }
    public void OnCloseFinishRound1()
    {
        RootFinishRound1.SetActive(false);
        if (_OncloseRound1 != null)
        {
            _OncloseRound1();
        }
    }




    public void InitConfirm(string mes, Action onYes, Action OnNo)
    {
        _OnYes = onYes;
        _OnNo = OnNo;
        txtConfrimText.text = mes;
        RootConfirm.SetActive(true);

    }
    public void OnYes()
    {
        RootConfirm.SetActive(false);
        if (_OnYes != null)
        {
            _OnYes();
        }
    }
    public void OnNo()
    {
        RootConfirm.SetActive(false);
        if (_OnNo != null)
        {
            _OnNo();
        }
    }

    public GameObject rootLoading;
    public void ShowLoading()
    {
        rootLoading.SetActive(true);
    }
    public void HideLoading()
    {
        rootLoading.SetActive(false);
    }
}
