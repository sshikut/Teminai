using TMPro;
using UnityEngine;

public class AnomalyStatusUI : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    public AnomalyMethod anomalyMethod; // 슬롯 제공자

    [Header("Save")]
    [SerializeField] private string saveKey = "AnomalyCollectedCount"; // 저장 키

    private int collected; // 지금까지 모은 개수(슬롯 트리거 횟수)

    private void Awake()
    {
        if (!anomalyMethod) anomalyMethod = FindObjectOfType<AnomalyMethod>();
        collected = PlayerPrefs.GetInt(saveKey, 0); // 저장본 로드

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
        UpdateText();
    }

    private void OnSlotTriggered(int index)
    {
        collected++;                                 // 카운트만 증가
        PlayerPrefs.SetInt(saveKey, collected);      // 즉시 저장
        PlayerPrefs.Save();
        UpdateText();
    }

    public void Refresh()
    {
        // 외부에서 새로고침 요청 시 텍스트만 갱신
        UpdateText();
    }

    private void UpdateText()
    {
        int total = (anomalyMethod != null) ? anomalyMethod.SlotCount : 0;
        // 총 슬롯 개수가 줄었을 때 표시만 안전하게 캡
        int show = Mathf.Min(collected, Mathf.Max(0, total));
        if (statusText) statusText.text = $"{show}/{total}";
    }

    // 새 게임 시 호출: 진행도 리셋
    public void ClearProgress()
    {
        collected = 0;
        PlayerPrefs.DeleteKey(saveKey);
        PlayerPrefs.Save();
        UpdateText();
    }
}
