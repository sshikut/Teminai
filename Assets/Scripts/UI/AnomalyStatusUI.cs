using TMPro;
using UnityEngine;

public class AnomalyStatusUI : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private bool pollEveryFrame = true;

    [Header("이상현상 감시할 오브젝트들")]
    [SerializeField] private GameObject[] targets;

    private bool[] initialStates;
    private bool[] triggered; // 한번이라도 발생한 적 있는지 기록

    private void Awake()
    {
        int len = targets.Length;
        initialStates = new bool[len];
        triggered = new bool[len];

        for (int i = 0; i < len; i++)
        {
            if (targets[i]) initialStates[i] = targets[i].activeSelf;
        }
    }

    private void Start()
    {
        if (!pollEveryFrame) Refresh();
    }

    private void Update()
    {
        if (pollEveryFrame) Refresh();
    }

    public void Refresh()
    {
        if (!statusText) return;

        int total = targets.Length;
        int current = 0;

        for (int i = 0; i < total; i++)
        {
            if (!targets[i]) continue;

            // 초기 상태와 다르고, 아직 카운트 안 됐다면 기록
            if (!triggered[i] && targets[i].activeSelf != initialStates[i])
            {
                triggered[i] = true;
            }

            if (triggered[i]) current++;
        }

        statusText.text = $"{current}/{total}";
    }
}
