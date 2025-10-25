using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleUI : MonoBehaviour
{
    public TextMeshProUGUI subtitle;

    private Coroutine subtitleCoroutine;

    public void SubtitleStart(string text, float duration)
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }
        subtitleCoroutine = StartCoroutine(ShowSubtitle(text, duration));
    }

    IEnumerator ShowSubtitle(string text, float duration)
    {
        subtitle.gameObject.SetActive(true);
        subtitle.text = text;

        yield return new WaitForSeconds(duration);

        subtitle.gameObject.SetActive(false);
        subtitle.text = "";
    }
}
