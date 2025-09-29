using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AirConditioner : MonoBehaviour, IInteractable
{
    public AudioClip onSound;
    public AudioClip offSound;
    public AudioSource airConSound;
    public bool isOn = false;

    public void Interact()
    {
        AirCon();
    }

    void AirCon()
    {
        if (isOn)
        {
            airConSound.PlayOneShot(offSound);
            isOn = false;
        }
        else
        {
            airConSound.PlayOneShot(onSound);
            isOn = true;
        }
            
    }
}
