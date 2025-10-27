using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class AnomalyStatusUI : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> statusTexts = new(); // 여러 개 지원
    public AnomalyMethod anomalyMethod;

    [Header("Save")]
    [SerializeField] private string saveKey = "AnomalyCollectedCount";

    private int collected;

    private void Awake()
    {
        if (!anomalyMethod) anomalyMethod = FindObjectOfType<AnomalyMethod>();
        collected = PlayerPrefs.GetInt(saveKey, 0);

        if (anomalyMethod != null)
            anomalyMethod.SlotTriggered += OnSlotTriggered;
    }

    private void OnDestroy()
    {
        if (anomalyMethod != null)
            anomalyMethod.SlotTriggered -= OnSlotTriggered;
    }

    private void Start()
    {
        UpdateTexts();
    }

    private void OnSlotTriggered(int index)
    {
        collected++;
        PlayerPrefs.SetInt(saveKey, collected);
        PlayerPrefs.Save();
        UpdateTexts();
    }

    public void Refresh()
    {
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        int total = (anomalyMethod != null) ? anomalyMethod.SlotCount : 0;
        int show = Mathf.Min(collected, Mathf.Max(0, total));

        bool unlocked = PlayerPrefs.GetInt("AnomalyLoopCleared", 0) == 1;

        foreach (var t in statusTexts)
        {
            if (!t) continue;
            t.text = unlocked ? $"{show}/{total}" : $"??/{total}";
        }
    }

    public void ClearProgress()
    {
        collected = 0;
        PlayerPrefs.DeleteKey(saveKey);
        PlayerPrefs.Save();
        UpdateTexts();
    }
}
