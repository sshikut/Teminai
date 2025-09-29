using System;
using UnityEngine;
using UnityEngine.Events;

public class AnomalyMethod : MonoBehaviour
{
    public event Action<int> SlotTriggered;

    // 동작 3종만: 켜기 / 끄기 / (현재 상태 기준) 토글
    public enum ActionType
    {
        Enable,
        Disable,
        Toggle
    }

    [Serializable]
    public class AnomalyItem
    {
        //public UnityEvent onInvokeAction;
        public GameObject target;   // 대상 오브젝트
        public ActionType action;   // 적용 동작
        [HideInInspector] public bool initialActive; // ← 초기 상태 저장 (추가)
    }

    [Serializable]
    public class AnomalySlot
    {
        public string label;          // 보기용 이름(선택)
        public AnomalyItem[] Objects; // 슬롯 내 개별 동작들
        public UnityEvent onSlotInvoked;
    }

    [Header("Anomaly 슬롯")]
    public AnomalySlot[] slots;

   

    // 시작 시 한 번 초기 상태 저장
    private void Awake()
    {
        CacheInitialStates();
    }

    // 슬롯/아이템들의 초기 activeSelf 상태 저장
    private void CacheInitialStates()
    {
        if (slots == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            var slot = slots[i];
            if (slot == null || slot.Objects == null) continue;

            for (int j = 0; j < slot.Objects.Length; j++)
            {
                var it = slot.Objects[j];
                if (it != null && it.target != null)
                {
                    it.initialActive = it.target.activeSelf;
                }
            }
        }
    }

    // 모든 이상현상 초기화: 전부 false가 아니라 "처음 씬 상태"로 복원
    public void InitAnomaly()
    {
        if (slots != null)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                var slot = slots[i];
                if (slot == null || slot.Objects == null) continue;

                for (int j = 0; j < slot.Objects.Length; j++)
                {
                    var it = slot.Objects[j];
                    if (it != null && it.target != null)
                    {
                        it.target.SetActive(it.initialActive); // ★ 변경: false → initialActive
                    }
                }
            }
        }

       
    }

    // 인덱스로 특정 슬롯 실행
    public void TriggerAnomaly(int index)
    {
        if (slots == null) return;
        if (index < 0 || index >= slots.Length) return;

        var slot = slots[index];
        if (slot == null || slot.Objects == null) return;

        for (int j = 0; j < slot.Objects.Length; j++)
        {
            var it = slot.Objects[j];
            if (it == null || it.target == null) continue;

            switch (it.action)
            {
                case ActionType.Enable:
                    it.target.SetActive(true);
                    break;
                case ActionType.Disable:
                    it.target.SetActive(false);
                    break;
                case ActionType.Toggle:
                    it.target.SetActive(!it.target.activeSelf); // 현재 상태 기준 토글
                    break;
            }
        }

        slot.onSlotInvoked?.Invoke();
        // SlotTriggered?.Invoke(index);
    }

    // 강제로 on/off (슬롯의 모든 아이템에 일괄 적용)
    public void SetAnomalyActive(int index, bool active)
    {
        if (slots == null) return;
        if (index < 0 || index >= slots.Length) return;

        var slot = slots[index];
        if (slot == null || slot.Objects == null) return;

        for (int j = 0; j < slot.Objects.Length; j++)
        {
            var it = slot.Objects[j];
            if (it != null && it.target != null)
                it.target.SetActive(active);
        }
    }

    // 슬롯 개수
    public int SlotCount
    {
        get { return (slots == null) ? 0 : slots.Length; }
    }

    // 슬롯 접근자(상태 UI 등에서 사용)
    public AnomalySlot GetSlot(int index)
    {
        if (slots == null || index < 0 || index >= slots.Length) return null;
        return slots[index];
    }
}
