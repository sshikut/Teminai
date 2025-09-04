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

        string newText = "이상 현상 : " + anomalyManager.isAnomaly + "\n"
                             + "학점 : " + grade + "\n"
                             + "LoopCount : " + anomalyManager.loopCount + "\n"
                             + "결석 수 : " + anomalyManager.absentCount + "\n"
                             + "남은 이상 현상: " + anomalyManager.remainAnomaly + "개";

        statusText.text = newText;
    }
}
