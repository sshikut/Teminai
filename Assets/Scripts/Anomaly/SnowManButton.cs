using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SnowManButton : MonoBehaviour, IInteractable
{
    public GameObject snowMan;

    public bool isOn = false;

    public void Interact()
    {
        AirCon();
    }

    void AirCon()
    {
        isOn = !isOn;

        if (isOn)
        {
            AudioManager.instance.Play("AirOnSound");
            snowMan.gameObject.SetActive(true);
        }
        else
        {
            AudioManager.instance.Play("AirOffSound");
        }
    }
}
