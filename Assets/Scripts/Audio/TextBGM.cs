using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextBGM : MonoBehaviour
{
    public int playMusicTrack;

    void Start()
    {
        BGMManager.instance .Play(playMusicTrack);
        BGMManager.instance.FadeInMusic();
    }
}
