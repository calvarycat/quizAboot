using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuiz : MonoBehaviour {
    public static UIQuiz instance;
    public List<Quizz> listQuiz;
    public GameObject PrefabsQuiz;
    public Transform RootQuiz;
    // Use this for initialization
    private void Awake()
    {
        instance = this;
    }
    void Start () {
        InitQuizUI();

    }
    public void OnShow(bool isShow)
    {
        RootQuiz.gameObject.SetActive(isShow);

    }
	
	public void InitQuizUI()
    {
        foreach(Quizz q in listQuiz)
        {
            GameObject obj = Instantiate(PrefabsQuiz, RootQuiz);
            QuizzItem qI = obj.GetComponent<QuizzItem>();
            qI.Init(q, OnQuestionClick);
            //obj.Init();
        }
    }
    public void OnQuestionClick(Quizz q)
    {

        OnShow(false);
        QuizzDetail.instance.OnInitQuiz(q);
        Debug.Log("qiz" + q.keyword);
    }
}
