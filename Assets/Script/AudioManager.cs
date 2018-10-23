using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;   
    public AudioSource audioSoundBackGround;
    public AudioSource audioSoundEffect;
    public AudioClip clipRight, clipWrong,button,congratulation,FinishRound1;


    public void Awake()
    {
        instance = this;

    }
    public void PlaySound(AudioClip clip)
    {

        audioSoundEffect.PlayOneShot(clip);
    }
	public void StopBGSound()
    {
        audioSoundBackGround.Stop();
    }
    public void PlayBgSound()
    {
        audioSoundBackGround.Play();
    }
    public void MuteSound(bool ismute)
    {

        audioSoundBackGround.mute = ismute;
        audioSoundEffect.mute = ismute;
    }
    public void PlayRightSound()
    {
        audioSoundEffect.PlayOneShot(clipRight);
    }
    public void PlayWrongSound()
    {
        Handheld.Vibrate();
        // audioSoundEffect.PlayOneShot(clipWrong);
    }
    public void PlayButtonClick()
    {
       // audioSoundEffect.PlayOneShot(button);
    }
    public void PlayCongratulation()
    {
        audioSoundEffect.PlayOneShot(congratulation);
    }
    public void PlayFinishRound1()
    {
        audioSoundEffect.PlayOneShot(FinishRound1);
    }
}
