using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    [Header("Mixers")]
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    // ───────── 슬라이더 OnValueChanged(float) 에 직접 연결 ─────────
    public void SetMusicVolume(float value)
    {
        if (musicMixer)
            musicMixer.audioMixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }

    public void SetSFXVolume(float value)
    {
        if (sfxMixer)
            sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }
}
