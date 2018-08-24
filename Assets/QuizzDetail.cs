using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizzDetail : MonoBehaviour
{

    public static QuizzDetail instance;
    public delegate void LetterButtonPressed();
    public static event LetterButtonPressed OnLetterButtonPressed;


    public GameObject Root;
    public Quizz quizz;
    public int QuizID;
    const int totalLettersPanel = 14;

    List<int> randomNumbers1 = new List<int>() { 3, 11, 5, 1, 8, 0, 2, 6, 12, 9, 4, 7, 10, 13 };
    List<int> randomNumbers2 = new List<int>() { 8, 13, 9, 1, 10, 0, 2, 5, 12, 6, 4, 7, 3, 11 };
    List<int> randomNumbers3 = new List<int>() { 6, 10, 9, 3, 13, 11, 2, 5, 0, 8, 4, 7, 1, 12 };
    List<List<int>> randomNumbers;
    int nextIndex = 0;                                  //Vamos numerando las letras de la respuesta para poder rellenarlas despues
    public bool bFullAnswer;                            //Se han rellenado todas las letras de la respuesta
    public int currentIndex;                            //Indice de la letra siguiente a rellenar
    public string currentLetter;
    public ButtonPanelCtrl currentButtonPanelCtrl;
    List<ButtonLetterCtrl> listButtonLetterCtrl;        //Lista para recorrer y buscar posibles huecos
    List<ButtonPanelCtrl> listButtonPanelCtrl;			//Lista para recorrer y resolver letras

    public GameObject panelLettersOneLine;
    public GameObject panelLettersTwoLines01;
    public GameObject panelLettersTwoLines02;
    public GameObject panelButtons;

    public GameObject buttonLetterAnswerPrefab;
    public GameObject buttonSpacePrefab;

    public void Awake()
    {
        instance = this;
    }

    public void OnInitQuiz(Quizz _quiz)
    {
        Root.SetActive(true);
        quizz = _quiz;
        Debug.Log("init quizz detail");
        Init();
    }
    public void OnClose()
    {
        UIQuiz.instance.OnShow(true);
        Root.gameObject.SetActive(false);
    }

    public void ReSet()
    {
        DeleteChildInParrent(panelLettersOneLine.transform);
        DeleteChildInParrent(panelLettersTwoLines01.transform);
        DeleteChildInParrent(panelLettersTwoLines02.transform);

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
       // ReSet();
        randomNumbers = new List<List<int>>();
        randomNumbers.Add(randomNumbers1);
        randomNumbers.Add(randomNumbers2);
        randomNumbers.Add(randomNumbers3);
        listButtonLetterCtrl = new List<ButtonLetterCtrl>();
        listButtonPanelCtrl = new List<ButtonPanelCtrl>();

        FillListButtonPanelCtrl();
        currentAnswer = quizz.keyword;
        words = quizz.keyword.Split(' '); ;
        PopulateAnswerPanel();
        PopulateLettersPanel();

    }
    void PopulateAnswerPanel()
    {
        //One word
        if (words.Length == 1)
        {
            panelLettersOneLine.SetActive(true);
            panelLettersTwoLines01.SetActive(false);
            panelLettersTwoLines02.SetActive(false);
            FillOneLine(words[0], panelLettersOneLine);
        }
        //Two or more words
        else
        {
            panelLettersOneLine.SetActive(false);
            panelLettersTwoLines01.SetActive(true);
            panelLettersTwoLines02.SetActive(true);

            //Caso especial para las respuestas que contienen "&"
            if (currentAnswer.Contains("&"))
            {
                FillOneLine(words[0] + " &", panelLettersTwoLines01);
                FillOneLine(words[2], panelLettersTwoLines02);
            }
            //Caso especial para las respuestas que contienen "-"
            else if (currentAnswer.Contains("-"))
            {
                FillOneLine(words[0] + "-", panelLettersTwoLines01);
                FillOneLine(words[2], panelLettersTwoLines02);
            }
            else
            {
                FillOneLine(words[0], panelLettersTwoLines01);
                FillOneLine(words[1], panelLettersTwoLines02);
            }
        }
    }
    void PopulateLettersPanel()
    {

        string cleanAnswer = currentAnswer.Replace(" ", "");
        cleanAnswer = cleanAnswer.Replace("&", "");
        cleanAnswer = cleanAnswer.Replace("-", "");

        //Cogemos aleatoriamente una de las 3 listas de numeros para colocar las letras en el panel
        List<int> positions = randomNumbers[Random.Range(0, 3)];
        for (int i = 0; i < cleanAnswer.Length; i++)
        {
            Button currentButton = panelButtons.transform.GetChild(positions[i]).GetComponent<Button>();
            currentButton.transform.GetChild(0).GetComponent<Text>().text = cleanAnswer[i].ToString();
        }
        //Terminamos de rellenar los botones que faltan con letras aleatorias
        for (int i = cleanAnswer.Length; i < totalLettersPanel; i++)
        {
            int num = Random.Range(0, 26);
            char letter = (char)('A' + num);
            Button currentButton = panelButtons.transform.GetChild(positions[i]).GetComponent<Button>();
            currentButton.transform.GetChild(0).GetComponent<Text>().text = letter.ToString();
        }
    }
    void FillOneLine(string word, GameObject currentPanel)
    {
        for (int i = 0; i < word.Length; i++)
        {
            GameObject buttonLetter;
            //Si es un espacio ocultamos el boton
            if (word[i] == ' ')
            {
                buttonLetter = Instantiate(buttonSpacePrefab) as GameObject;
            }
            else
            {
                buttonLetter = Instantiate(buttonLetterAnswerPrefab) as GameObject;
            //   buttonLetter.GetComponent<ButtonLetterCtrl>().ShowButton();

                Text text = buttonLetter.transform.GetChild(0).GetComponent<Text>();
                text.text = word[i].ToString();
               
                //Si es un "&" lo mostramos
                if (word[i] == '&')
                {
                    buttonLetter.transform.GetComponent<Button>().interactable = false;
                }
                //Si es un "-" lo mostramos
                else if (word[i] == '-')
                {
                    buttonLetter.transform.GetComponent<Button>().interactable = false;
                }
                //Las letras normales las ocultamos
                else
                {
                    ButtonLetterCtrl buttonLetterCtrl = buttonLetter.GetComponent<ButtonLetterCtrl>();
                    buttonLetterCtrl.index = nextIndex;
                    buttonLetterCtrl.SetAnswer(text.text);
                    listButtonLetterCtrl.Add(buttonLetterCtrl);
                    nextIndex++;
                    text.text = "";
                }
            }
            buttonLetter.transform.SetParent(currentPanel.transform);
            buttonLetter.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            buttonLetter.transform.localPosition = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    void FillListButtonPanelCtrl()
    {
        ButtonPanelCtrl[] array = panelButtons.GetComponentsInChildren<ButtonPanelCtrl>();
        foreach (ButtonPanelCtrl buttonPanelCtrl in array)
        {
            listButtonPanelCtrl.Add(buttonPanelCtrl);
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
    bool CheckLevelIsFinished()
    {
        bool correct = true;
        foreach (ButtonLetterCtrl buttonLetterCtrl in listButtonLetterCtrl)
        {
            if (!buttonLetterCtrl.CheckAnswer())
            {
                correct = false;
                break;
            }
        }
        return correct;
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
