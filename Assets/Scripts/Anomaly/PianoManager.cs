using UnityEngine;

public class PianoManager : MonoBehaviour
{
    private readonly string[] target = {
        "Do","Do","Sol","Sol","La","La","Sol","Fa","Fa","Mi","Mi","Re","Re","Do"
    };

    [Tooltip("정답 시 비활성화할 문 오브젝트")]
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

        // 정답 검사
        if (target[index] == note)
        {
            index++;
            Debug.Log($"[PianoMelodyPuzzle] 정답! 다음 인덱스 → {index}");

            if (index >= target.Length)
            {
                cleared = true;
                Debug.Log("[PianoMelodyPuzzle] 퍼즐 클리어!! 문 비활성화 시도");

                if (doorToDisable != null)
                {
                    doorToDisable.SetActive(false);
                    Debug.Log("[PianoMelodyPuzzle] 문 비활성화 성공!");
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