using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoxWorld : MonoBehaviour
{
    public GameObject rootHide;
    //public GameObject rootOpen;
    public GameObject rootNotOpen;
    public GameObject RootHightLight;
    public GameObject RootSpecial;
    public GameObject SelectObject;
    public Text txtLableNomal;
    public Text txtLableRight;
    public Text txtLableHightLight;
  //  public bool isHightLight;
    bool IsOpen;
    Action<int> AcGoToQuiz;
    int id; // id cau hoi
    public int keyMaping;
    public int idButton;
    public bool isCanSelect;
    public  string rightAnser;
    public void Init(string lb, bool isHide, bool isstart,bool isHilight , Action<int> acGotoQuiz,int _id,int _idButton)
    {
        id = _id;
        idButton = _idButton;
        SetActiveObject(false, false, false, false, false);
        if (isHide)
        {
            HideBox();
        }
        else
        {
            isCanSelect = true;
            AcGoToQuiz = acGotoQuiz; // set action
            rootNotOpen.gameObject.SetActive(true);          
            if (isHilight)
            {
                RootHightLight.gameObject.SetActive(true);
            }
            else
            {
                RootHightLight.gameObject.SetActive(false);
            }
            if (isstart)
            {
                SetActiveObject(true, false, false, false, false);

            }
            rightAnser = lb.ToUpper();
        }
    }
   public void SetActiveObject(bool isSpecial,bool isHide,bool isOpen,bool isNotOpen,bool isHightlight)
    {
        RootSpecial.SetActive(isSpecial);
        rootHide.SetActive(isHide);     
        rootNotOpen.gameObject.SetActive(isNotOpen);
        RootHightLight.gameObject.SetActive(isHightlight);
    }
    public void HideBox()
    {
        rootHide.SetActive(true);     
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
            rootHide.SetActive(false);

        }
    }
    public void SetSelect(bool isSelect)
    {
        SelectObject.SetActive(isSelect);
    }
    public void OnClick()
    {
        if (!IsOpen)
        {
            if (AcGoToQuiz != null)
            {
                AcGoToQuiz(id);
            }
        }
    }
    public void OnMapingKey(int keyID,string text)
    {
       // Debug.Log(keyID);
        keyMaping = keyID;
        txtLableNomal.text = text.ToUpper();
        txtLableRight.text = text.ToUpper();
        txtLableHightLight.text = text.ToUpper();
    //    Debug.Log("keyID" + keyID + "Text" + text);

    }
    public void OnUnMapingKey(int keyID, string text)
    {
      //  Debug.Log(keyID);
        keyMaping = -1;
        txtLableNomal.text = "";
        txtLableRight.text = "";
        txtLableHightLight.text = "";

    }

    public bool CheckRightAnswer(string ans)
    {
        if (rightAnser == ans)
            return true;
        return false;
    }


}
