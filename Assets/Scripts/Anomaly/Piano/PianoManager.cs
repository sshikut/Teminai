using UnityEngine;

public class PianoManager : MonoBehaviour
{
    private readonly string[] target = {
        "Do","Do","Sol","Sol","La","La","Sol","Fa","Fa","Mi","Mi","Re","Re","Do"
    };

    [Tooltip("정답 시 열릴 문 오브젝트 (Animator 포함)")]
    public GameObject doorToDisable;

    private int index = 0;
    private bool cleared = false;

    public void InputNote(string note)
    {
        Debug.Log($"[PianoMelodyPuzzle] 입력됨: {note}, 현재 인덱스: {index}");

        if (cleared)
        {
            Debug.Log("[PianoMelodyPuzzle] 이미 클리어됨, 무시");
            return;
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
                        AudioManager.instance.Play("Open");
                        anim.Play("Open"); // 애니메이션 재생
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
