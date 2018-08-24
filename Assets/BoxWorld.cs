using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoxWorld : MonoBehaviour
{
    public GameObject rootHide;
    public GameObject rootOpen;
    public GameObject rootNotOpen;  
    public GameObject RootHightLight;

    public Text txtLableNomal;
    public Text txtLableRight;
    public Text txtLableHightLight;
    public bool isHightLight;
    bool IsOpen;
    Action AcGoToQuiz;
    public void Init(string lb, bool isHide, bool isstart, bool isEnd,Action acGotoQuiz)
    {
        if (isHide)
        {
            HideBox();
        }
        else
        {
            AcGoToQuiz = acGotoQuiz; // set action
            rootNotOpen.gameObject.SetActive(true);
            txtLableNomal.text = lb.ToUpper();
            txtLableRight.text = lb.ToUpper();
            txtLableHightLight.text = lb.ToUpper() ;
            if (isHightLight)
            {   
                RootHightLight.gameObject.SetActive(true);
            }else
            {
                RootHightLight.gameObject.SetActive(false);
            }

        }

    }
    public void HideBox()
    {
        rootHide.SetActive(true);
        rootOpen.SetActive(false);
        rootNotOpen.SetActive(false);
        rootHide.SetActive(false);
        RootHightLight.SetActive(false);
    }
    public void ChangeOpen(bool _isOpen)
    {
        if (_isOpen)
        {
            IsOpen = true;
            rootNotOpen.SetActive(false);
            rootOpen.SetActive(true);
            rootHide.SetActive(false);

        }
        //else
        //{
        //    rootOpen.SetActive(false);
        //    rootHide.SetActive(true);
        //}

    }
    public void OnClick()
    {
        if(!IsOpen)
        {
            Debug.Log("vào trong trang kia");
            if(AcGoToQuiz!=null)
            {
                AcGoToQuiz();
            }
        }
    }
    



}
