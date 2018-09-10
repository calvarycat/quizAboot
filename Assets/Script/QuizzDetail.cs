﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizzDetail : MonoBehaviour
{

    public static QuizzDetail instance;
    public delegate void LetterButtonPressed();
    public static event LetterButtonPressed OnLetterButtonPressed;

    public List<GenerateWrods> listGenerate = new List<GenerateWrods>();
    int currentSelectQuizz = 0;
    public GameObject Root;
    public Quizz quizz;
    public int QuizID;
    const int totalLettersPanel = 15;
    List<List<int>> randomNumbers;
    int nextIndex = 0;                                  //Vamos numerando las letras de la respuesta para poder rellenarlas despues
    public bool bFullAnswer;                            //Se han rellenado todas las letras de la respuesta
    public int currentIndex;                            //Indice de la letra siguiente a rellenar
    public string currentLetter;
    [HideInInspector]
    public ButtonPanelCtrl currentButtonPanelCtrl;
    List<ButtonPanelCtrl> listButtonPanelCtrl;			//Lista para recorrer y resolver letras
    public GameObject panelButtons;
    public GameObject buttonLetterAnswerPrefab;
    public GameObject buttonSpacePrefab;
    public Text txtHint;

    public void Awake()
    {
        instance = this;
    }

    public void OnSHow(bool isShow)
    {
        Root.SetActive(isShow);
    }
    public void OnInitQuiz(Quizz _quiz, int idQuiz)
    {
        currentSelectQuizz = idQuiz;
        txtHint.text = _quiz.hints[0];
        quizz = _quiz;
        if (!listGenerate[idQuiz].isGenerate)
        {
            listGenerate[idQuiz].isGenerate = true;
            Root.SetActive(true);          
            Init();
        }
        else
        {
            for (int i = 0; i < totalLettersPanel; i++)
            {

                Button currentButton = panelButtons.transform.GetChild(i).GetComponent<Button>();
                currentButton.transform.GetChild(0).GetComponent<Text>().text = listGenerate[currentSelectQuizz].charGen[i].ToString();
                if (GetFillButton(i))
                {
                    panelButtons.transform.GetChild(i).GetComponent<ButtonPanelCtrl>().HideButton();
                }
                else
                {
                    panelButtons.transform.GetChild(i).GetComponent<ButtonPanelCtrl>().ShowButton();
                }
            }
        }
    }
    public bool GetFillButton(int idButton)
    {
        QuizzItem q = UIQuiz.instance.listQuizzItem[currentSelectQuizz];
        for (int i = 0; i < q.transform.childCount; i++)
        {
            BoxWorld b = q.transform.GetChild(i).GetComponent<BoxWorld>();
            if (b.keyMaping == idButton)
            {
                return true;
            }
        }
        return false;


    }
    public void OnClose()
    {
        UIQuiz.instance.OnShow(true);
        Root.gameObject.SetActive(false);
    }

    public void ReSet()
    {
        nextIndex = 0;                                  //Vamos numerando las letras de la respuesta para poder rellenarlas despues
        bFullAnswer = false;                            //Se han rellenado todas las letras de la respuesta
                                                        //Indice de la letra siguiente a rellenar
        currentLetter = "";
        currentIndex = 0;

    }
    public void DeleteChildInParrent(Transform tran)
    {
        foreach (Transform t in tran.transform)
        {
            Destroy(t.gameObject);
        }
    }
    string[] words;
    string currentAnswer;
    public void Init()
    {

        randomNumbers = new List<List<int>>();
        currentAnswer = quizz.keyword.ToUpper();
        words = quizz.keyword.Split(' ');
        listGenerate[currentSelectQuizz].keyword = currentAnswer;
        PopulateLettersPanel();


    }
    void PopulateLettersPanel()
    {
        string cleanAnswer = currentAnswer.Replace(" ", "");
        cleanAnswer = cleanAnswer.Replace("&", "");
        cleanAnswer = cleanAnswer.Replace("-", "");
        List<int> positions = Util.GetRandomNumberInList(15);
        for (int i = 0; i < cleanAnswer.Length; i++)
        {
            Button currentButton = panelButtons.transform.GetChild(positions[i]).GetComponent<Button>();
            currentButton.transform.GetChild(0).GetComponent<Text>().text = cleanAnswer[i].ToString();
            listGenerate[currentSelectQuizz].charGen[positions[i]] = cleanAnswer[i].ToString();
        }
        int d = 0;
        for (int i = cleanAnswer.Length; i < totalLettersPanel; i++)
        {
            int num = Random.Range(0, 26);
            char letter = (char)('A' + num);
            d++;
            if (cleanAnswer.Contains("1") && d % 3 == 0)
            {
                string a = Random.Range(2, 9).ToString();
                letter = a[0];
            }


            Button currentButton = panelButtons.transform.GetChild(positions[i]).GetComponent<Button>();
            currentButton.transform.GetChild(0).GetComponent<Text>().text = letter.ToString();
            listGenerate[currentSelectQuizz].charGen[positions[i]] = letter.ToString();
        }
        for (int i = 0; i < panelButtons.transform.childCount; i++)
        {
            ButtonPanelCtrl btn = panelButtons.transform.GetChild(i).GetComponent<ButtonPanelCtrl>();
            btn.SetLetterEnable();
        }
    }

    public void SetLetter(string letter, int idButton)
    {
        if (listGenerate[currentSelectQuizz].isFullAnswer)
        {
            return;
        }

        UIQuiz.instance.OnFillText(currentSelectQuizz, idButton, letter);

        if (CheckLevelIsFinished())
        {
            AudioManager.instance.PlayRightSound();
            listGenerate[currentSelectQuizz].isCorrect = true;
            PopUpmanager.instance.InitExplain(quizz.explain, OnCloseExplain);
            SHowMedal();
            return;
        }
        if (CheckFullAnswer())
        {
            AudioManager.instance.PlayWrongSound();
        }

    }
    public void OnCloseExplain()
    {
        if (CheckFinishRound1())
        {

            AppControl.instance.round = 2;
            QuizzPart2.instance.InitQuizz();

        }
    }
    public void UnSetLetter(string letter, int idButton)
    {

        UIQuiz.instance.OnUnFillText(currentSelectQuizz, idButton, letter);
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

    public bool CheckFinishRound1()
    {
        for (int i = 0; i < listGenerate.Count; i++)
        {
            if (listGenerate[i].isCorrect == false)
            {
                return false;
            }
        }
        return true;
    }
    public Transform parentMedal;
    public void SHowMedal()
    {
        for (int i = 0; i < listGenerate.Count; i++)
        {
            if (listGenerate[i].isCorrect == true)
            {
                parentMedal.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                parentMedal.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }

    }

    bool CheckLevelIsFinished()
    {

        bool rs = true;
        QuizzItem q = UIQuiz.instance.listQuizzItem[currentSelectQuizz];
        for (int i = 0; i < q.transform.childCount; i++)
        {
            BoxWorld b = q.transform.GetChild(i).GetComponent<BoxWorld>();
            if (b.idButton >= 0)
            {
                // char a = listGenerate[currentSelectQuizz].keyword[b.idButton];
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

    public bool CheckFullAnswer()
    {

        bool rs = true;
        QuizzItem q = UIQuiz.instance.listQuizzItem[currentSelectQuizz];
        for (int i = 0; i < q.transform.childCount; i++)
        {
            BoxWorld b = q.transform.GetChild(i).GetComponent<BoxWorld>();
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

    //public void SetNextIndex()
    //{
    //    bFullAnswer = false;

    //    foreach (ButtonLetterCtrl buttonLetterCtrl in listButtonLetterCtrl)
    //    {
    //        if (buttonLetterCtrl.text.text == "")
    //        {
    //            currentIndex = buttonLetterCtrl.index;
    //            return;
    //        }
    //    }
    //    bFullAnswer = true;
    //}


}
[System.Serializable]
public class GenerateWrods
{
    public string keyword;
    public List<string> charGen = new List<string>(15);
    public int currentSelectItem;
    public bool isGenerate;
    public bool isCorrect;
    public bool isFullAnswer;
}