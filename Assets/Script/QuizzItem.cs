using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[System.Serializable]
public class QuizzItem : MonoBehaviour
{
   
    public Quizz quiz;
    public Transform root;
    public Text txtOder;
    public Action<Quizz,int> _OnQuizzClick;
    public int quidID;

    public void Init(Quizz q, Action<Quizz,int> onQuizzClick, int quizzOder = 0)
    {
        quidID = quizzOder;
        quiz = q;
        _OnQuizzClick = onQuizzClick;      
        int num = 0;
        for (int i = 0; i < root.childCount; i++)
        {
            BoxWorld bx = root.GetChild(i).GetComponent<BoxWorld>();

            int startto = q.startFrom + q.keyword.Length;
            if (i >= q.startFrom && i < startto)
            {
                bx.Init(q.keyword[num].ToString(), false, false,false, GotoQuiz, quidID, num);
                if(i==q.keyhightlight)
                {
                    bx.Init(q.keyword[num].ToString(), false,false,true, GotoQuiz, quidID, num);
                }
                num++;
            }
            else
            {
                bx.Init("", true, false,false, GotoQuiz, quidID,-1);
            }
            if (q.keySetStart == i || q.keysetEnd == i)
            {
                bx.Init("", false, true,false, GotoQuiz, quidID,-1);
            }

        }
    }



    public bool CheckQuizz(string text)
    {      
        if (text.Trim().ToUpper() == quiz.keyword.Trim().ToUpper())
        {
            return true;
        }
        return false;

    }
    public void GotoQuiz(int quidID)
    {

        if (_OnQuizzClick != null)
        {
            _OnQuizzClick(quiz,quidID);
        }
    }
   
}
