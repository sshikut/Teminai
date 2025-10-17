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
}
