using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizzPart2 : MonoBehaviour
{
    public static QuizzPart2 instance;
    public GameObject root;
    // Use this for initialization
    public Quizz2 q;
    string currentAnswer;
    public Transform panelButtons;
    public GameObject pnCongratulation;
    public GameObject pnGamePlay, pnHoanThanh, pnTryAgain;

    private void Awake()
    {

        instance = this;
        FillAnswer();
    }

    public void OnShow(bool isShow)
    {

        root.SetActive(false);
    }
    public void Resert()
    {
        FillAnswer();
    }
    public void InitQuizz()
    {
        Resert();
      
        AppControl.instance.round = 2;
        root.SetActive(true);
        pnCongratulation.gameObject.SetActive(true);
        pnGamePlay.gameObject.SetActive(false);
         StartCoroutine(OpenGamePlay());
    }
    IEnumerator OpenGamePlay()
    {   
        yield return new WaitForSeconds(5);
        if (pnCongratulation.activeSelf)
        {
           
            AudioManager.instance.PlaySoundGame2();
            pnCongratulation.gameObject.SetActive(false);
            pnGamePlay.gameObject.SetActive(true);
          
        }
    }
    public void StartGame2()
    {
        if (pnCongratulation.activeSelf)
        {
            pnCongratulation.gameObject.SetActive(false);
            pnGamePlay.gameObject.SetActive(true);
            AudioManager.instance.PlaySoundGame2();
        }

    }
    public void FillAnswer()
    {
        PopulateLettersPanel();
        ResetAns();
    }


    public bool CheckFullAnswer()
    {

        bool rs = true;

        for (int i = 0; i < ans.transform.childCount; i++)
        {
            BoxWorld b = ans.transform.GetChild(i).GetComponent<BoxWorld>();
            if (b.idButton >= 0)
            {
                if (!string.IsNullOrEmpty(b.txtLableNomal.text))
                {
                    rs = true;
                }
                else
                {
                    rs = false;
                    break;
                }
            }
        }
        return rs;

    }


    public void ResetAns()
    {
        for (int i = 0; i < ans.transform.childCount; i++) // vì ở đây không có dấu sao hàng cuối
        {
            BoxWorld b = ans.transform.GetChild(i).GetComponent<BoxWorld>();
            b.Unmapkey();
            b._isSelect = true;
            b.HideLastItem();
        }
    }


    void PopulateLettersPanel()
    {
        currentAnswer = q.AvailableKey;
        string cleanAnswer = currentAnswer.Replace(" ", "");
        cleanAnswer = cleanAnswer.Replace("&", "");
        cleanAnswer = cleanAnswer.Replace("-", "");


        List<int> positions = Util.GetRandomNumberInList(q.AvailableKey.Length);
        for (int i = 0; i < cleanAnswer.Length; i++)
        {
            Button currentButton = panelButtons.transform.GetChild(positions[i]).GetComponent<Button>();
            currentButton.transform.GetChild(1).GetComponent<Text>().text = cleanAnswer[i].ToString();
        }


        for (int i = 0; i < panelButtons.transform.childCount; i++)
        {
            ButtonPanelCtrl btn = panelButtons.transform.GetChild(i).GetComponent<ButtonPanelCtrl>();
            btn.SetLetterEnable();
        }
    }

    public void SetLetter(string letter, int idButton)
    {
        OnFillText(idButton, letter);

        Invoke("RunAfterSeconf", .2f);
    }
    public void RunAfterSeconf()
    {
        if (CheckFinishRound2())
        {
            AppControl.instance.gamestate = GameState.Stop;
            AudioManager.instance.PlayRightSound();
            Invoke("OpenpnHoanThanh", 1f);
            return;
        }
        if (CheckFullAnswer())
        {
            AudioManager.instance.PlayWrongSound();
        }
    }
    public void OpenpnHoanThanh()
    {
        AudioManager.instance.PlayCongratulation();
        AppControl.instance.SendData();
        pnHoanThanh.SetActive(true);
        pnGamePlay.SetActive(false);
        Invoke("OpenPnTryAgain", 2f);

    }
    public void OpenPnTryAgain()
    {
        pnHoanThanh.SetActive(false);
        pnTryAgain.SetActive(true);
    }
    bool CheckFinishRound2()
    {
        bool rs = true;
        for (int i = 0; i < ans.transform.childCount; i++)
        {
            BoxWorld b = ans.transform.GetChild(i).GetComponent<BoxWorld>();
            if (b.idButton >= 0)
            {
                if (b.txtLableNomal.text == b.rightAnser)
                {
                    rs = true;
                }
                else
                {
                    rs = false;
                    break;
                }
            }
        }
        return rs;
    }
    public void OnCloseExplain()
    {
        Debug.Log("Send score to server");
        pnHoanThanh.gameObject.SetActive(true);
    }
    public void PanelTryAgain()
    {
        pnHoanThanh.gameObject.SetActive(false);
        pnTryAgain.SetActive(true);
    }
    public void OnHoanThanhClick()
    {
        pnTryAgain.SetActive(false);
        AppControl.instance.StartNewSession();
    }
    public void UnSetLetter(string letter, int idButton)
    {
        for (int i = 0; i < ans.transform.childCount; i++)
        {

            BoxWorld b = ans.transform.GetChild(i).GetComponent<BoxWorld>();
            b.HideLastItem();
            if (idButton == b.keyMaping)
            {
                b.ShowLastItem();
                // b.OnUnMapingKey(idButton, letter);
                b.Unmapkey();
            }
        }
    }
    public GameObject panelButtonsAns;
    public void UnserAnswer(int id)
    {

        for (int i = 0; i < panelButtonsAns.transform.childCount; i++)
        {
            ButtonPanelCtrl b = panelButtonsAns.transform.GetChild(i).GetComponent<ButtonPanelCtrl>();
            if (id == b.idButton)
            {
                b.ShowButton();
            }
        }
    }


    public GameObject ans;
    void OnFillText(int idButton, string letter)
    {

        for (int i = 0; i < ans.transform.childCount; i++) // vì ở đây không có dấu sao hàng cuối
        {
            BoxWorld b = ans.transform.GetChild(i).GetComponent<BoxWorld>();
            b.HideLastItem();
            if (b.keyMaping == -1 && b.idButton >= 0)
            {
                b.ShowLastItem();
                b.OnMapingKey(idButton, letter);
                return;
            }
        }
    }
}

[System.Serializable]
public class Quizz2
{
    public string AvailableKey;
    public string keyword;
    public List<string> hints;
    public string explain;
}