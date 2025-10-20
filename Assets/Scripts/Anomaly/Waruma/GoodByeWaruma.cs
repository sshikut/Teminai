using System.Collections;
using UnityEngine;

public class GoodByeWaruma : MonoBehaviour
{
    private Vector3 originalScale; // 원래 크기 저장

    private void Awake()
    {
        originalScale = transform.localScale; // 시작 시 크기 기억
    }

    private void OnEnable()
    {
        // 비활성화 후 다시 켜질 때 원래 크기로 복원
        transform.localScale = originalScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에 들어옴!");
            StartCoroutine(ShrinkAndDisappear());
        }
    }

    private IEnumerator ShrinkAndDisappear()
    {
        float duration = 1f; // 1초 동안 축소
        float t = 0f;
        Vector3 startScale = transform.localScale;

        while (t < duration)
        {
            float p = t / duration;
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, p);
            t += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
