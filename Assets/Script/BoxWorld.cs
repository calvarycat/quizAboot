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
    public string rightAnser;
    public void Init(string lb, bool isHide, bool isstart, bool isHilight, Action<int> acGotoQuiz, int _id, int _idButton)
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
    public void SetActiveObject(bool isSpecial, bool isHide, bool isOpen, bool isNotOpen, bool isHightlight)
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
   public bool _isSelect;
    public void SetSelect(bool isSelect)
    {
        _isSelect = isSelect;
        SelectObject.SetActive(isSelect);
    }
    public void OnClick()
    {
        Debug.Log("On unclick" + _isSelect);
        if (_isSelect)
            OnUnMapKeyFromWordBox();
        else
        {
            if (AcGoToQuiz != null)
            {
                AcGoToQuiz(id);
            }
        }
      

    }
    void OnUnMapKeyFromWordBox()
    {
        if (AppControl.instance.round == 1)
            QuizzDetail.instance.UnserAnswer(keyMaping);
        else
            QuizzPart2.instance.UnserAnswer(keyMaping);

        keyMaping = -1;
        if (txtLableNomal)
            txtLableNomal.text = "";
        if (txtLableRight)
            txtLableRight.text = "";
        if (txtLableHightLight)
            txtLableHightLight.text = "";

    }
    public void OnMapingKey(int keyID, string text)
    {
        keyMaping = keyID;
        if (txtLableNomal)
            txtLableNomal.text = text.ToUpper();
        if (txtLableRight)
            txtLableRight.text = text.ToUpper();
        if (txtLableHightLight)
            txtLableHightLight.text = text.ToUpper();

    }
    //public void OnUnMapingKey(int keyID, string text)
    //{
    //    keyMaping = -1;
    //    if (txtLableNomal)
    //        txtLableNomal.text = "";
    //    if (txtLableRight)
    //        txtLableRight.text = "";
    //    if (txtLableHightLight)
    //        txtLableHightLight.text = "";

    //}
    public void Unmapkey()
    {
        keyMaping = -1;
        if (txtLableNomal)
            txtLableNomal.text = "";
        if (txtLableRight)
            txtLableRight.text = "";
        if (txtLableHightLight)
            txtLableHightLight.text = "";

    }

    public bool CheckRightAnswer(string ans)
    {
        if (rightAnser == ans)
            return true;
        return false;
    }


}
