using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    [Tooltip("깜빡이게 할 TextMeshPro UI 요소")]
    public TextMeshProUGUI textToBlink;

    [Tooltip("깜빡이는 속도 (초 단위)")]
    public float blinkSpeed = 0.5f;

    private Coroutine blinkCoroutine;

    private void OnEnable()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        blinkCoroutine = StartCoroutine(Blink());
    }

    private void OnDisable()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        textToBlink.alpha = 1f;
    }

    private IEnumerator Blink()
    {
        while (true) // 무한 반복
        {
            textToBlink.alpha = 0f;
            yield return new WaitForSeconds(blinkSpeed);

            textToBlink.alpha = 1f;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }
}
