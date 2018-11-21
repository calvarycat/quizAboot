using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    public AudioSource audioSoundBackGround;
    public AudioSource audioSoundEffect;
    public AudioClip clipRight, clipWrong, button, congratulation, FinishRound1;
    public AudioClip audioRound1, audioround2;


    public void Awake()
    {
        instance = this;
    }
    public void PlaySoundGame1()
    {
        audioSoundBackGround.clip = audioRound1;
        audioSoundBackGround.Play();
    }
    public void PlaySoundGame2()
    {

        audioSoundBackGround.clip = audioround2;
        audioSoundBackGround.Stop();
        StartCoroutine(PlayAfterSecond());
    }
    public IEnumerator PlayAfterSecond()
    {
        yield return new WaitForSeconds(1f);
        audioSoundBackGround.Play();
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
