using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuiz : MonoBehaviour
{
    public static UIQuiz instance;
    public Transform Root;  
    public List<Quizz> listQuiz;
    public GameObject PrefabsQuiz;
    public Transform RootQuiz; 
    public List<QuizzItem> listQuizzItem;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InitQuizUI();

    }
    public void OnShow(bool isShow)
    {
        Root.gameObject.SetActive(isShow);

        OnQuestionClick(listQuiz[0], 0);
    }

    public void InitQuizUI()
    {
        listQuizzItem = new List<QuizzItem>();
        int quizzoder = 0;
        foreach (Quizz q in listQuiz)
        {
            GameObject obj = Instantiate(PrefabsQuiz, RootQuiz);
            QuizzItem qI = obj.GetComponent<QuizzItem>();
            qI.Init(q, OnQuestionClick, quizzoder);
            quizzoder++;
            listQuizzItem.Add(qI);
        }

    }
    int curentselectqID = 0;
    public void OnQuestionClick(Quizz q, int quizid)
    {
        curentselectqID = quizid;
        QuizzDetail.instance.OnInitQuiz(q, quizid);
        SetSelectQuiz(quizid);
    }
    public void SetSelectQuiz(int id)
    {
        for (int i = 0; i < listQuizzItem.Count; i++)
        {
            for (int j = 0; j < listQuizzItem[i].transform.childCount - 1; j++)
            {
                if (i == id)
                {
                    if (!listQuizzItem[i].transform.GetChild(j).GetComponent<BoxWorld>().isCanSelect)
                    {
                        listQuizzItem[i].transform.GetChild(j).GetComponent<BoxWorld>().SetSelect(false);
                    }
                    else
                    {
                        listQuizzItem[i].transform.GetChild(j).GetComponent<BoxWorld>().SetSelect(true);
                    }
                }
                else
                {
                    listQuizzItem[i].transform.GetChild(j).GetComponent<BoxWorld>().SetSelect(false);
                }

            }
        }
    }

    public void OnFillText(int currentQuiz, int keyAnsID, string text)
    {

        if (listQuizzItem.Count > 0)
        {

            QuizzItem q = listQuizzItem[currentQuiz];
            for (int i = 0; i < q.transform.childCount - 1; i++)
            {

                BoxWorld b = q.transform.GetChild(i).GetComponent<BoxWorld>();
            
                //if  (QuizzDetail.instance.listGenerate[currentQuiz].currentSelectItem == b.idButton)
                //{
                if(b.keyMaping==-1 && b.idButton>=0)
                {
               //     Debug.Log("no khong vo day luon");
                    b.OnMapingKey(keyAnsID, text);
                    return;
                }
                   
                   
               // }
            }
            QuizzDetail.instance.listGenerate[currentQuiz].currentSelectItem = QuizzDetail.instance.listGenerate[currentQuiz].currentSelectItem + 1;
        }
        else
        {
            Debug.Log("null data");
        }
    }
    public void OnUnFillText(int currentQuiz, int keyAnsID, string text)
    {

        if (listQuizzItem.Count > 0)
        {

            QuizzItem q = listQuizzItem[currentQuiz];
            for (int i = 0; i < q.transform.childCount - 1; i++)
            {

                BoxWorld b = q.transform.GetChild(i).GetComponent<BoxWorld>();
                if (keyAnsID == b.keyMaping)
                {                   
                    b.OnUnMapingKey(keyAnsID, text);
                }
            }
        }
        else
        {
            Debug.Log("null data");
        }
    }

}
