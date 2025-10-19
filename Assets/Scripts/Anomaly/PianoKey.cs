using UnityEngine;

public class PianoKey : MonoBehaviour
{
    public string soundName = "Do";   // 누를 때 재생할 효과음 이름
    public float pressDepth = 0.02f;  // 얼마나 눌릴지
    public float pressSpeed = 10f;    // 눌림 속도

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
            // 사운드 재생
            AudioManager.instance.Play(soundName);

            // 눌림 애니메이션 실행
            StartCoroutine(PressKey());
        }
    }

    private System.Collections.IEnumerator PressKey()
    {
        isPressed = true;
        Vector3 target = originalPosition - new Vector3(0, pressDepth, 0);

        // 아래로 눌림
        while (Vector3.Distance(transform.localPosition, target) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * pressSpeed);
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        // 다시 위로
        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.001f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * pressSpeed);
            yield return null;
        }

        isPressed = false;
    }
}