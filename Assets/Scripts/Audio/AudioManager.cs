using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    private AudioSource source;

    public string name;   // 사운드 이름
    public AudioClip clip;
    public float Volumn = 1f;
    public bool loop;

    public void SetSource(AudioSource _source, AudioMixerGroup mixer)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = Volumn;

        if (mixer != null)
            source.outputAudioMixerGroup = mixer;
    }

    public void SetVolume()
    {
        if (source != null)
            source.volume = Volumn;
    }

    public void Play()
    {
        if (source == null) return;
        source.Play();
    }

    public void Stop()
    {
        if (source == null) return;
        source.Stop();
    }

    public void SetLoop() => source.loop = true;
    public void SetLoopCancel() => source.loop = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Mixer Groups")]
    public AudioMixerGroup sfxMixer;  // 여기에 믹서 연결 (Inspector에서 할당)

    [SerializeField]
    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            transform.SetParent(null);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("Sound_" + sounds[i].name);
            soundObject.transform.SetParent(this.transform);

            // SFX 믹서 연결 추가
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>(), sfxMixer);
        }
    }

    public void Play(string _name)
    {
        foreach (var s in sounds)
        {
            if (s.name == _name)
            {
                s.Play();
                return;
            }
        }
    }

    public void Stop(string _name)
    {
        foreach (var s in sounds)
        {
            if (s.name == _name)
            {
                s.Stop();
                return;
            }
        }
    }

    public void SetLoop(string _name)
    {
        foreach (var s in sounds)
        {
            if (s.name == _name)
            {
                s.SetLoop();
                return;
            }
        }
    }

    public void SetLoopCancel(string _name)
    {
        foreach (var s in sounds)
        {
            if (s.name == _name)
            {
                s.SetLoopCancel();
                return;
            }
        }
    }

    public void SetVolume(string _name, float _Volumn)
    {
        foreach (var s in sounds)
        {
            if (s.name == _name)
            {
                s.Volumn = _Volumn;
                s.SetVolume();
                return;
            }
        }
    }
}
