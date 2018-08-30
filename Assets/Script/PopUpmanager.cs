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
    public Text txtcontentExplain;
    public Action _OncloseExplain;

    public GameObject RootFinishRound1;
    public Text txtContentRound1;
    public Action _OncloseRound1;

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
        _OncloseExplain = onClose;
        txtcontentExplain.text = mes;
        rootExplain.SetActive(true);

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

}
