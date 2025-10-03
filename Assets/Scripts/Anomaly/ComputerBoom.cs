using System.Collections;
using UnityEngine;

public class ComputerBoom : MonoBehaviour, IInteractable
{
    [Header("NPC Setting")]
    public Communication student;

    [Header("Target Setting")]
    public Renderer objectRenderer;
    public Material blueScreen;

    [Header("Effect Setting")]
    public float emissionFadeInDuration = 1f;
    public float emissionHoldDuration = 3f;
    public float blueScreenHoldDuration = 5f;

    [Header("SFX Setting")]
    public AudioClip boomWindowSound;
    public AudioSource computerAudio;

    private const int TARGET_MATERIAL_INDEX = 1;
    private Material targetMaterialInstance;
    private Coroutine runningCoroutine;

    private Color originalBaseColor;
    private Color originalEmissionColor;
    private Texture originalBaseMap;

    private bool isPlaying = false;

    void Awake()
    {
        targetMaterialInstance = objectRenderer.materials[TARGET_MATERIAL_INDEX];

        originalBaseColor = targetMaterialInstance.GetColor("_BaseColor");
        originalEmissionColor = targetMaterialInstance.GetColor("_EmissionColor");
        originalBaseMap = targetMaterialInstance.GetTexture("_BaseMap");
    }

    public void Interact()
    {
        if (isPlaying) return;

        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
        }

        runningCoroutine = StartCoroutine(AnomalySequence());
    }

    IEnumerator AnomalySequence()
    {
        isPlaying = true;

        if (computerAudio != null && boomWindowSound != null)
        {
            computerAudio.PlayOneShot(boomWindowSound);
        }

        float elapsedTime = 0f;
        Color startEmission = targetMaterialInstance.GetColor("_EmissionColor");
        Color targetEmission = startEmission + new Color(20 / 255f, 20 / 255f, 20 / 255f);

        while (elapsedTime < emissionFadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            Color currentColor = Color.Lerp(startEmission, targetEmission, elapsedTime / emissionFadeInDuration);
            targetMaterialInstance.SetColor("_EmissionColor", currentColor);
            yield return null;
        }

        targetMaterialInstance.SetColor("_EmissionColor", targetEmission);

        yield return new WaitForSeconds(emissionHoldDuration);

        if (blueScreen != null)
        {
            Texture blueScreenTexture = blueScreen.GetTexture("_BaseMap");
            targetMaterialInstance.SetTexture("_BaseMap", blueScreenTexture);
        }

        student.PlayDialogue("\"어?\"", 30f, 180f);

        targetMaterialInstance.SetColor("_EmissionColor", Color.black);

        yield return new WaitForSeconds(blueScreenHoldDuration);

        targetMaterialInstance.SetColor("_BaseColor", Color.black);
        targetMaterialInstance.SetColor("_EmissionColor", Color.black);

        runningCoroutine = null; // 코루틴 종료
    }

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += ResetComputer;
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= ResetComputer;
    }

    public void ResetComputer()
    {
        isPlaying = false;
        if (targetMaterialInstance == null) return;

        targetMaterialInstance.SetColor("_BaseColor", originalBaseColor);
        targetMaterialInstance.SetColor("_EmissionColor", originalEmissionColor);
        targetMaterialInstance.SetTexture("_BaseMap", originalBaseMap);
    }
}