using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YgerDisco : MonoBehaviour
{
    [Header("Emission 설정")]
    public Material target; 
    public float changeInterval = 0.5f; 
    public float transitionDuration = 0.4f; 
    public float minIntensity = 0.5f; 
    public float maxIntensity = 2f;   

    private Coroutine discoCoroutine;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Material Error!", this);
            enabled = false;
            return;
        }

        if (!target.HasProperty("_EmissionColor"))
        {
            Debug.LogError("머티리얼에 '_EmissionColor' 프로퍼티가 없습니다. 스크립트를 비활성화합니다.", this);
            enabled = false;
            return;
        }

        discoCoroutine = StartCoroutine(ChangeEmissionColorRoutine());
    }

    private IEnumerator ChangeEmissionColorRoutine()
    {
        while (true) // 무한 반복
        {
            Color startColor = target.GetColor("_EmissionColor");

            Color targetBaseColor = new Color(Random.value, Random.value, Random.value, 1f);

            float targetIntensity = Random.Range(minIntensity, maxIntensity);

            Color targetEmissionColor = targetBaseColor * targetIntensity;

            float timer = 0f;
            while (timer < transitionDuration)
            {
                Color currentColor = Color.Lerp(startColor, targetEmissionColor, timer / transitionDuration);
                target.SetColor("_EmissionColor", currentColor);

                timer += Time.deltaTime;
                yield return null;
            }
            target.SetColor("_EmissionColor", targetEmissionColor);

            yield return new WaitForSeconds(changeInterval);
        }
    }

    void OnDisable()
    {
        if (discoCoroutine != null)
        {
            StopCoroutine(discoCoroutine);
        }
    }
}
