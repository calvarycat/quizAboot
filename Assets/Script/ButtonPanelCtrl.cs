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
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	void Awake()
	{
		button = GetComponent<Button>();
		image = GetComponent<Image>();
		text = transform.GetChild(0).GetComponent<Text>();
	}
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public void OnButtonPressed()
	{

        AudioManager.instance.PlayButtonClick();
        if (!QuizzDetail.instance.bFullAnswer){
            if(!bHide)
            {
                HideButton();
                StartCoroutine(SetLetter());
            }else
            {
                ShowButton();
                StartCoroutine(UnSetLetter());
            }
         
            
        }else
        {
            Debug.Log("Full answer");
        }
	}
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void HideButton()
	{
        // button.interactable = false;
      
         //      button.enabled = false;
         //image.enabled = false;
         //text.enabled = false;
         bHide = true;
	}
   
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ShowButton()
	{
		//button.enabled = true;
		//image.enabled = true;
		//text.enabled = true;
		bHide = false;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Nos esperamos un frame para que el audio no suene con retraso
	IEnumerator SetLetter()
	{
		yield return null;
        QuizzDetail.instance.SetLetter(text.text, idButton);
    }
    IEnumerator UnSetLetter()
    {
        yield return null;
        QuizzDetail.instance.UnSetLetter(text.text, idButton);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Volvemos a mostrar una letra del panel si el usuario la ha borrado de la respuesta
    public void SetLetterEnable()
	{
		//button.enabled = true;
		//image.enabled = true;
		//text.enabled = true;
        bHide=false;
	}
}
