using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public string soundName = "Do"; // "Do","Re","Mi","Fa","Sol","La" 처럼 사용
    public float pressDepth = 0.02f;
    public float pressSpeed = 10f;

    public PianoManager puzzle; // 퍼즐 참조 추가

    private Vector3 originalPosition;
    private bool isPressed = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void OnMouseDown()
    {
        if (!isPressed)
        {
            // 퍼즐에 입력 전달 (야매 방식: 틀리면 퍼즐이 알아서 인덱스 0으로 리셋)
            if (puzzle != null) puzzle.InputNote(soundName);

            // 사운드 + 눌림 애니메이션
            AudioManager.instance.Play(soundName);
            StartCoroutine(PressKey());
        }
    }

    private System.Collections.IEnumerator PressKey()
    {
        isPressed = true;
        Vector3 target = originalPosition - new Vector3(0, pressDepth, 0);

        while (Vector3.Distance(transform.localPosition, target) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * pressSpeed);
            yield return null;
        }
        yield return new WaitForSeconds(0.05f);
        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * pressSpeed);
            yield return null;
        }
        isPressed = false;
    }
}