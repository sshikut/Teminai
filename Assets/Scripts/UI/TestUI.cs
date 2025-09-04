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
        string[] grades = {"F", "D", "D+", "C", "C+", "B", "B+", "A", "A+" };
        string grade = "";

        if (anomalyManager.loopCount < grades.Length) 
            grade = grades[anomalyManager.loopCount];

        string newText = "�̻� ���� : " + anomalyManager.isAnomaly + "\n"
                             + "���� : " + grade + "\n"
                             + "LoopCount : " + anomalyManager.loopCount + "\n"
                             + "�Ἦ �� : " + anomalyManager.absentCount + "\n"
                             + "���� �̻� ����: " + anomalyManager.remainAnomaly + "��";

        statusText.text = newText;
    }
}
