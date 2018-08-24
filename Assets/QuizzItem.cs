using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuizzItem : MonoBehaviour {
    [HideInInspector]
    public Quizz quiz;
    public Transform root;
    public Text txtOder;
    public Action<Quizz> _OnQuizzClick;
   
    public void Init(Quizz q, Action<Quizz> onQuizzClick, int quizzOder=0)
    {
        quiz = q;
        _OnQuizzClick = onQuizzClick;
        //root.GetChild(0).GetComponent
        int num = 0;
       for (int i=1;i<root.childCount;i++)
        {
            BoxWorld bx = root.GetChild(i).GetComponent<BoxWorld>();
           
            int startto = q.startFrom + q.keyword.Length;
            if (i>=q.startFrom && i<startto)
            {
               
              //  Debug.Log(i);
                bx.Init(q.keyword[num].ToString(),false,false,false, GotoQuiz);
                num++;
            }
            else
            {
                bx.Init("", true, false, false, GotoQuiz);
            }
           
           
        }
    }
    public bool CheckQuizz(string text)
    {
        if(text.Trim().ToUpper()==quiz.keyword.Trim().ToUpper())
        {
            return true;
        }
        return false;
        
    }
    public void GotoQuiz()
    {
       
        if(_OnQuizzClick!=null)
        {
            _OnQuizzClick(quiz);
        }
    }
}
