using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Controlador para las letras del panel
public class ButtonPanelCtrl : MonoBehaviour
{
    Button button;
    Image image;
    public Text text;
    public bool bHide;
    public int idButton;
    public GameObject HintRight;


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text = transform.GetChild(1).GetComponent<Text>();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void OnButtonPressed()
    {
        AudioManager.instance.PlayButtonClick();

        if (!bHide)
        {
            if (AppControl.instance.round == 1)
            {
                if (QuizzDetail.instance.CheckFullAnswer())
                {
                    return;
                }
                HideButton();
                StartCoroutine(SetLetter());
            }
            else
            {

                if (QuizzPart2.instance.CheckFullAnswer())
                {
                    return;
                }
                HideButton();
                StartCoroutine(SetLetter());
            }



        }
        else
        {
            ShowButton();
            StartCoroutine(UnSetLetter());
        }



    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void HideButton()
    {
        if (GetComponent<Image>().color == Color.yellow)
        {
            GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            GetComponent<Image>().color = Color.grey;
        }

        bHide = true;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ShowButton()
    {
        if (GetComponent<Image>().color == Color.yellow || GetComponent<Image>().color == Color.cyan)
        {
            GetComponent<Image>().color = Color.yellow;
        }
        else
        {

            GetComponent<Image>().color = Color.white;
        }

        bHide = false;
    }

    public void ShowHint()
    {

        if (HintRight)
            HintRight.SetActive(true);
    }
    public void HideHint()
    {
        if (HintRight)
            HintRight.SetActive(false);
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Nos esperamos un frame para que el audio no suene con retraso
    IEnumerator SetLetter()
    {
        yield return null;
        if (AppControl.instance.round == 1)
            QuizzDetail.instance.SetLetter(text.text, idButton);
        else
            QuizzPart2.instance.SetLetter(text.text, idButton);
    }
    IEnumerator UnSetLetter()
    {
        yield return null;
        if (AppControl.instance.round == 1)
            QuizzDetail.instance.UnSetLetter(text.text, idButton);
        else
            QuizzPart2.instance.UnSetLetter(text.text, idButton);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Volvemos a mostrar una letra del panel si el usuario la ha borrado de la respuesta
    public void SetLetterEnable()
    {
        GetComponent<Image>().color = Color.white;

        bHide = false;
    }
}
