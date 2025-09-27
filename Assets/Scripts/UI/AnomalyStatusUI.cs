using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnomalyStatusUI : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    public AnomalyMethod anomalyMethod; // 슬롯 제공자

    // 슬롯 단위로 트리거 여부
    private bool[] triggered;

    // 초기 상태 스냅샷: 슬롯별(아이템별) 활성 상태 저장
    private bool[][] initialStatesBySlot;

    private void Awake()
    {
        if (!anomalyMethod) anomalyMethod = FindObjectOfType<AnomalyMethod>();
        CaptureInitialStates();

        // ★ 이벤트 구독 (슬롯이 실행되면 즉시 수집 처리)
        if (anomalyMethod != null)
            anomalyMethod.SlotTriggered += OnSlotTriggered;
    }

    private void Start()
    {
        UpdateText();
    }

    private void OnDestroy()
    {
        // ★ 구독 해제
        if (anomalyMethod != null)
            anomalyMethod.SlotTriggered -= OnSlotTriggered;
    }

    // ★ 슬롯이 실행되면 바로 카운트 올리고 텍스트 갱신
    private void OnSlotTriggered(int index)
    {
        if (triggered == null) return;
        if (index < 0 || index >= triggered.Length) return;

        triggered[index] = true;
        UpdateText();
    }

    // 슬롯/아이템들의 초기 activeSelf 상태를 저장
    private void CaptureInitialStates()
    {
        int len = (anomalyMethod != null) ? anomalyMethod.SlotCount : 0;

        triggered = new bool[len];
        initialStatesBySlot = new bool[len][];

        for (int i = 0; i < len; i++)
        {
            var slot = anomalyMethod.GetSlot(i);

            if (slot == null || slot.Objects == null)
            {
                initialStatesBySlot[i] = new bool[0];
                continue;
            }

            int itemCount = slot.Objects.Length;
            initialStatesBySlot[i] = new bool[itemCount];

            for (int j = 0; j < itemCount; j++)
            {
                var item = slot.Objects[j];
                var go = (item != null) ? item.target : null;
                initialStatesBySlot[i][j] = (go != null) ? go.activeSelf : false;
            }
        }
    }

    public void Refresh()
    {
        if (!statusText || anomalyMethod == null) return;

        int total = anomalyMethod.SlotCount;

        // 슬롯 개수가 바뀌었을 수 있으니 재스냅샷
        if (triggered == null || triggered.Length != total ||
            initialStatesBySlot == null || initialStatesBySlot.Length != total)
        {
            CaptureInitialStates();
        }

        // (선택) 기존 비교로도 감지하고 싶으면 유지
        for (int i = 0; i < total; i++)
        {
            var slot = anomalyMethod.GetSlot(i);
            if (slot == null || slot.Objects == null) continue;

            if (!triggered[i])
            {
                bool changed = false;
                int itemCount = slot.Objects.Length;

                if (initialStatesBySlot[i] == null || initialStatesBySlot[i].Length != itemCount)
                {
                    changed = true;
                }
                else
                {
                    for (int j = 0; j < itemCount; j++)
                    {
                        var item = slot.Objects[j];
                        var go = (item != null) ? item.target : null;

                        bool now = (go != null) ? go.activeSelf : false;
                        bool init = initialStatesBySlot[i][j];

                        if (now != init)
                        {
                            changed = true;
                            break;
                        }
                    }
                }

                if (changed) triggered[i] = true;
            }
        }

        UpdateText();
    }

    private void UpdateText()
    {
        int total = (triggered != null) ? triggered.Length : 0;
        int current = 0;
        if (triggered != null)
        {
            for (int i = 0; i < triggered.Length; i++)
                if (triggered[i]) current++;
        }
        statusText.text = $"{current}/{total}";
    }
}
