using UnityEngine;

public class PianoManager : MonoBehaviour
{
    private readonly string[] target = {
        "Do","Do","Sol","Sol","La","La","Sol","Fa","Fa","Mi","Mi","Re","Re","Do"
    };

    [Tooltip("피아노 UI/오브젝트 루트")]
    public GameObject Piano;

    [Tooltip("정답 시 열릴 문 오브젝트 (Animator 포함)")]
    public GameObject doorToDisable;

    private int index = 0;
    private bool cleared = false;

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += resetPiano;

        // 켜질 때 초기화(선호 시 유지)
        ResetState();
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= resetPiano;
    }

    private void ResetState()
    {
        cleared = false;
        index = 0;
        Debug.Log("[PianoMelodyPuzzle] 상태 초기화");
    }

    public void resetPiano()
    {
        // 비활성화할 때도 즉시 초기화해야 다음에 켤 때 클리어가 남지 않음
        ResetState();
        if (Piano) Piano.SetActive(false);
    }

    // 필요하면 외부에서 이걸로 켜세요: 항상 새 판으로 시작
    public void ShowPiano()
    {
        ResetState();
        if (Piano) Piano.SetActive(true);
    }

    public void InputNote(string note)
    {
        Debug.Log($"[PianoMelodyPuzzle] 입력됨: {note}, 현재 인덱스: {index}");

        if (cleared)
        {
            Debug.Log("[PianoMelodyPuzzle] 이미 클리어됨, 무시");
            return;
        }

        // 방어코드: index가 범위를 벗어나지 않도록
        if (index < 0 || index >= target.Length)
        {
            Debug.LogWarning("[PianoMelodyPuzzle] 인덱스 범위 초과 → 상태 초기화");
            ResetState();
        }

        if (target[index] == note)
        {
            index++;
            Debug.Log($"[PianoMelodyPuzzle] 정답! 다음 인덱스 → {index}");

            if (index >= target.Length)
            {
                cleared = true;
                Debug.Log("[PianoMelodyPuzzle] 퍼즐 클리어!! 문 열기 시도");

                if (doorToDisable != null)
                {
                    Animator anim = doorToDisable.GetComponent<Animator>();
                    if (anim != null)
                    {
                        if (AudioManager.instance) AudioManager.instance.Play("Open");
                        anim.Play("Open");
                        Debug.Log("[PianoMelodyPuzzle] 문 애니메이션 'Open' 실행!");
                    }
                    else
                    {
                        Debug.LogWarning("[PianoMelodyPuzzle] Animator가 문 오브젝트에 없음");
                    }
                }
                else
                {
                    Debug.LogWarning("[PianoMelodyPuzzle] doorToDisable == null (문이 연결 안 됨)");
                }
            }
        }
        else
        {
            Debug.LogWarning($"[PianoMelodyPuzzle] 오답: {note}, 정답은 {target[index]} → 인덱스 리셋");
            index = 0;
        }
    }
}
