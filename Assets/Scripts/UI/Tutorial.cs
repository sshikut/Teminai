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
        if (UIGuard.isAnyUIOpen && !isOn) return;

        isOn = !isOn;
        UI.gameObject.SetActive(isOn);
        UIGuard.isAnyUIOpen = isOn;

        if (isOn)
        {
            // ★ Tab처럼
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // ★ Tab처럼
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (isOn && Input.GetKeyDown(KeyCode.Escape))
        {
            // 닫기
            isOn = false;
            UI.gameObject.SetActive(false);
            UIGuard.isAnyUIOpen = false;

            // ★ Tab처럼
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // ESC 입력이 TogglePhone에 전달되지 않게
            UIGuard.justClosedUI = true;
        }
    }
}