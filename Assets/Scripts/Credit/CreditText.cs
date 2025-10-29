using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TogglePhone;

public class CreditText : MonoBehaviour, IInteractable
{
    [TextArea]
    public string textString;
    public string nameString;
    public TMP_Text nameText;
    public TMP_Text text;
    public GameObject text_UI;
    public FirstPersonController firstPersonController;

    public bool isOpen = false;

    public void Interact()
    {
        ShowText();
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {

            text_UI.SetActive(false);

            
            if (firstPersonController) firstPersonController.cameraRotation = true;

        }
    }

    public void ShowText()
    {
        isOpen = true;
        text.text = textString;
        nameText.text = nameString;
        text_UI.SetActive(true);

        if (firstPersonController) firstPersonController.cameraRotation = false;
    }
}
