using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TogglePhone;

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
        if (UIGuard.isAnyUIOpen && !isOn) return; // 이미 다른 UI 열려있으면 무시

        isOn = !isOn;
        UI.gameObject.SetActive(isOn);

        UIGuard.isAnyUIOpen = isOn; // 열릴 때 true, 닫힐 때 false
    }
}
