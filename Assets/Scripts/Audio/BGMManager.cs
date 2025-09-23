using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;

    public AudioSource source;

    [Header("BGM")]

    public AudioClip[] clips; //배경음악들

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f); //이걸 만들면 한번만 new 생성자가 실행됨


    #region Singleton
    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion Singleton


    public void SetVolunmn(float _volumn)
    {
        source.volume = _volumn;    
    }

    public void Pause() 
    { 
        source.Pause();
    }
    public void UnPause()
    {
        source.UnPause();
    }

    public void Play(int _playMusicTrack) 
    {
        source.volume = 1f;
        source.clip = clips[_playMusicTrack]; //원하는 배열의 값을 재생
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void FadeOutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine() 
    {
        for (float i = 1.0f; i > 0; i -= 0.01f) 
        {
            source.volume = i;
            yield return waitTime;
        }
    }

    public void FadeInMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }

    IEnumerator FadeInMusicCoroutine()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            source.volume = i;
            yield return waitTime;
        }
    }
}


