using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AirConditioner : MonoBehaviour, IInteractable
{
    public bool isOn = false;

    public void Interact()
    {
        AirCon();
    }

    void AirCon()
    {
        isOn = !isOn;

        if (isOn)
            AudioManager.instance.Play("AirOnSound");
        else
            AudioManager.instance.Play("AirOffSound");
    }
}
