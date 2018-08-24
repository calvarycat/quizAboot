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
        Debug.Log("ansswer");
		if(!QuizzDetail.instance.bFullAnswer){
			HideButton();
			//AudioManager.instance.PlayAudio(AudioManager.Audios.ButtonClick);
			StartCoroutine(SetLetter());
		}
	}
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public void HideButton()
	{
		button.enabled = false;
		image.enabled = false;
		text.enabled = false;
		bHide = true;
	}
   
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ShowButton()
	{
		button.enabled = true;
		image.enabled = true;
		text.enabled = true;
		bHide = false;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Nos esperamos un frame para que el audio no suene con retraso
	IEnumerator SetLetter()
	{
		yield return null;
		QuizzDetail.instance.SetLetter(text.text, this);
	}
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Volvemos a mostrar una letra del panel si el usuario la ha borrado de la respuesta
	public void SetLetterEnable()
	{
		button.enabled = true;
		image.enabled = true;
		text.enabled = true;
	}
}
