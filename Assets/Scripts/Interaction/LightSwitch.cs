using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light[] lights;
    public bool isLightOn = true;

    public void Interact()
    {
        Switch();
    }

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += InitLight;
    }

    // 오브젝트가 비활성화될 때 구독을 해제합니다. (매우 중요!)
    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= InitLight;
    }

    public void Switch()
    {
        isLightOn = !isLightOn;

        // 상호작용할 때마다 좌우 반전
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        AudioManager.instance.Play("LightSwitch");
        float targetIntensity = isLightOn ? 2f : 0f;

        foreach (Light light in lights)
        {
            light.intensity = targetIntensity;
        }
    }

    public void InitLight()
    {
        if (!isLightOn)
        {
            isLightOn = true;
        }

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        float targetIntensity = isLightOn ? 2f : 0f;

        foreach (Light light in lights)
        {
            light.intensity = targetIntensity;
        }
    }
}
