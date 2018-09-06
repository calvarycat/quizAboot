﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Controlador para las letras de la respuesta
public class ButtonLetterCtrl : MonoBehaviour
{
	public int index;

	public Text text;
	public ButtonPanelCtrl buttonPanelCtrl;		//Letra del panel que se ha pulsado (linkamos para poder colocarla si el usuario la borra)
	public string answer;						//Letra que corresponde a la respuesta correcta
	public bool bCorrectForced;					//true si se ha forzado la respuesta pulsando el boton de resolver letra
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	void Awake()
	{
		text = transform.GetChild(0).GetComponent<Text>();
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Al crear la letra en la respuesta
	public void SetAnswer(string s)
	{
		answer = s;
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Se ha pulsado el boton de resolver una letra
	public void SetCorrectText()
	{
		text.text = answer;
		bCorrectForced = true;
		text.color = Color.green;
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public bool isCorrect()
	{
		return answer == text.text;
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	public bool CheckAnswer()
	{

        return (text.text.ToUpper() == answer.ToUpper());
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	void OnEnable()
	{
        QuizzDetail.OnLetterButtonPressed += SetLetter;
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	
	void OnDisable()
	{
        QuizzDetail.OnLetterButtonPressed -= SetLetter;
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Si esta es la siguiente letra a rellenar -> la rellenamos
	void SetLetter()
	{
		if(index== QuizzDetail.instance.currentIndex){
			text.text = QuizzDetail.instance.currentLetter;
			buttonPanelCtrl = QuizzDetail.instance.currentButtonPanelCtrl;
		}
	}
	
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//Si se pulsa una letra de la respuesta hay que quitar esa letra y volverla a poner en el panel
	public void OnButtonPressed()
	{
		if(!bCorrectForced && buttonPanelCtrl!=null){
			text.text = "";
			buttonPanelCtrl.SetLetterEnable();
            QuizzDetail.instance.SetNextIndex();
			//AudioManager.instance.PlayAudio(AudioManager.Audios.ButtonClickAlt);
		}
	}
    //tu them vo
   
}










