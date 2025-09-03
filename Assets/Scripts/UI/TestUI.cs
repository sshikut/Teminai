using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestUI : MonoBehaviour
{
    public AnomalyManager anomalyManager;
    public TMP_Text statusText;

    private void Update()
    {
        UpdateText();    
    }

    public void UpdateText()
    {
        string newText = "�̻� ���� : " + anomalyManager.isAnomaly + "\n"
                             + "LoopCount : " + GameManager.Instance.loopCount + "\n"
                             + "���� �̻� ����: " + anomalyManager.remainAnomaly + "��";

        statusText.text = newText;
    }
}
