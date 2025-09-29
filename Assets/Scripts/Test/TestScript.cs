using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public AnomalyManager anomalyManager;
    public GameObject testUI;
    public TMP_InputField indexInput;
    public TMP_Text testText;
    private bool isUIOpen = false;

    // 확정 이상현상

    // 출석 수, 결석 수 조정

    // 확정 RDS

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (!isUIOpen)
            {
                testUI.SetActive(true);
                isUIOpen = true;
            }
            else
            {
                testUI.SetActive(false);
                isUIOpen = false;
            }
                
        }

        testText.text = "Anomaly : ";
    }

    public void OnClick_PlaySelectedAnomaly()
    {
        if (int.TryParse(indexInput.text, out int index))
        {
            Debug.Log($"System: {index}번 이상현상 실행");
            anomalyManager.AnomalyTest(index); 
        }
        else
        {
            Debug.LogWarning("유효한 숫자를 입력해주세요.");
        }
    }

    public void OnClick_RDSSTest()
    {
        if (int.TryParse(indexInput.text, out int index))
        {
            Debug.Log($"System: {index}번 RDSS 실행");
            anomalyManager.RDSSTest(index);
        }
        else
        {
            Debug.LogWarning("유효한 숫자를 입력해주세요.");
        }
    }

    public void OnClick_SetLoopCount()
    {
        if (int.TryParse(indexInput.text, out int index))
        {
            Debug.Log($"System: LoopCount = {index}");
            anomalyManager.SetLoopCount(index);
        }
        else
        {
            Debug.LogWarning("유효한 숫자를 입력해주세요.");
        }
    }

    public void OnClick_SetAbsentCount()
    {
        if (int.TryParse(indexInput.text, out int index))
        {
            Debug.Log($"System: AbsentCount = {index}");
            anomalyManager.SetAbsentCount(index);
        }
        else
        {
            Debug.LogWarning("유효한 숫자를 입력해주세요.");
        }
    }
}
