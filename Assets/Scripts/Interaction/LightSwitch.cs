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
        float targetIntensity = isLightOn ? 2f : 0f;

        foreach (Light light in lights)
        {
            light.intensity = targetIntensity;
        }
    }
}
