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
        string newText = "이상 현상 : " + anomalyManager.isAnomaly + "\n"
                             + "LoopCount : " + GameManager.Instance.loopCount + "\n"
                             + "남은 이상 현상: " + anomalyManager.remainAnomaly + "개";

        statusText.text = newText;
    }
}
