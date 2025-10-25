using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleUI : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public float duration = 5f;

    private Coroutine subtitleCoroutine;

    public void SubtitleStart(string text)
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }
        subtitleCoroutine = StartCoroutine(ShowSubtitle(text));
    }

    IEnumerator ShowSubtitle(string text)
    {
        subtitle.gameObject.SetActive(true);
        subtitle.text = text;

        yield return new WaitForSeconds(duration);

        subtitle.gameObject.SetActive(false);
        subtitle.text = "";
    }
}
