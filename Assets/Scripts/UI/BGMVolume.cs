using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMVolume : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        // 슬라이더 초기값을 현재 볼륨과 동기화
        if (BGMManager.instance != null && BGMManager.instance.source != null)
        {
            slider.value = BGMManager.instance.source.volume;
        }

        // 값이 바뀔 때마다 볼륨 세팅
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        if (BGMManager.instance != null)
        {
            BGMManager.instance.SetVolunmn(value);
        }
    }
}
