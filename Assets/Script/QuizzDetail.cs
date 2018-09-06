using System.Collections;
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

    List<int> randomNumbers1 = new List<int>() { 3, 11, 14, 5, 1, 8, 0, 2, 6, 12, 9, 4, 7, 10, 13 };
    List<int> randomNumbers2 = new List<int>() { 8, 13, 9, 14, 1, 10, 0, 2, 5, 12, 6, 4, 7, 3, 11 };
    List<int> randomNumbers3 = new List<int>() { 6, 10, 9, 14, 3, 13, 11, 2, 5, 0, 8, 4, 7, 1, 12 };
    List<List<int>> randomNumbers;
    int nextIndex = 0;                                  //Vamos numerando las letras de la respuesta para poder rellenarlas despues
    public bool bFullAnswer;                            //Se han rellenado todas las letras de la respuesta
    public int currentIndex;                            //Indice de la letra siguiente a rellenar
    public string currentLetter;
    [HideInInspector]
    public ButtonPanelCtrl currentButtonPanelCtrl;
    List<ButtonLetterCtrl> listButtonLetterCtrl;        //Lista para recorrer y buscar posibles huecos
    List<ButtonPanelCtrl> listButtonPanelCtrl;			//Lista para recorrer y resolver letras
    public GameObject panelButtons;
    public GameObject buttonLetterAnswerPrefab;
    public GameObject buttonSpacePrefab;
    public Text txtHint;

    public void Awake()
    {
        instance = this;
    }

    public void OnInitQuiz(Quizz _quiz, int idQuiz)
    {
        currentSelectQuizz = idQuiz;
        txtHint.text = _quiz.hints[0];
        if (!listGenerate[idQuiz].isGenerate)
        {
            listGenerate[idQuiz].isGenerate = true;
            Root.SetActive(true);
            quizz = _quiz;
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
        randomNumbers.Add(randomNumbers1);
        randomNumbers.Add(randomNumbers2);
        randomNumbers.Add(randomNumbers3);
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

       
        List<int> positions = randomNumbers[Random.Range(0, 3)];
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



    public void SetLetter(string letter, ButtonPanelCtrl buttonPanelCtrl)
    {
        if (bFullAnswer)
        {
            return;
        }
        currentLetter = letter;
        currentButtonPanelCtrl = buttonPanelCtrl;

        if (OnLetterButtonPressed != null)
        {
            OnLetterButtonPressed();
        }
        if (CheckLevelIsFinished())
        {
            // StartCoroutine(GotoNextLevel());
            Debug.Log("is finish");
        }
        else
        {
            SetNextIndex();
        }
    }

    public void SetLetter(string letter, int idButton)
    {

        if (listGenerate[currentSelectQuizz].isFullAnswer)
        {
            Debug.Log("full rồi không làm gì cả");
            return;
        }

        UIQuiz.instance.OnFillText(currentSelectQuizz, idButton, letter);
        if (CheckLevelIsFinished())
        {
            // StartCoroutine(GotoNextLevel());
            listGenerate[currentSelectQuizz].isCorrect = true;
            PopUpmanager.instance.InitExplain(quizz.explain, OnCloseExplain);
            Debug.Log("finish quest rồi");
            SHowMedal();
        }
      
    }
    public void OnCloseExplain()
    {
        if (CheckFinishRound1())
        {
            string mes = "Chuc mung ban da thu thap dduowc du 8 huy hieu B-Nobel. Hayx tieep tuc chinh phuc thu thach tiep nao";
            PopUpmanager.instance.InitFinishRound1(mes, NextRound);

        }
    }
    public void NextRound()
    {
        Debug.Log("net round");
    }
    public void UnSetLetter(string letter, int idButton)
    {
        UIQuiz.instance.OnUnFillText(currentSelectQuizz, idButton, letter);
    }

    public bool CheckFinishRound1()
    {
        for(int i =0;i<listGenerate.Count;i++)
        {
            if(listGenerate[i].isCorrect==false)
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
                parentMedal.GetChild(i).gameObject.SetActive(true);
            }else
            {
                parentMedal.GetChild(i).gameObject.SetActive(false);
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
                }


            }
        }
        return rs;

    }

    public void SetNextIndex()
    {
        bFullAnswer = false;

        foreach (ButtonLetterCtrl buttonLetterCtrl in listButtonLetterCtrl)
        {
            if (buttonLetterCtrl.text.text == "")
            {
                currentIndex = buttonLetterCtrl.index;
                return;
            }
        }
        bFullAnswer = true;
    }


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