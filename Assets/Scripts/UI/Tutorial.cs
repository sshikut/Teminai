using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour, IInteractable
{
    public Image UI;

    public bool isOn = false;

    public void Interact()
    {
        OpenUI();
    }

    void OpenUI()
    {
        if (isOn)
        {
            UI.gameObject.SetActive(true);
            isOn = false;
        }
        else
        {
            UI.gameObject.SetActive(false);
            isOn = true;
        }

    }
}
