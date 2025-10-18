using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Communication : MonoBehaviour, IInteractable
{
    [Header("NPC Setting")]
    [SerializeField] private Rig headLookRig;
    [SerializeField] private Transform playerTarget;

    [SerializeField] private float lookDuration = 5f;
    // [SerializeField] private float lookSpeed = 5f;
    [SerializeField] private float rigWeightSpeed = 3f;

    [SerializeField] private float maxLookAngle = 80f;

    [Header("Dialogue Setting")]
    [SerializeField] private NPCDialogue dialogue;
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Coroutine lookCoroutine;
    private bool isLooking = false;

    private void Update()
    {
        bool canLookAtPlayer = IsPlayerInSight();

        float targetWeight = (isLooking && canLookAtPlayer) ? 1f : 0f;
        headLookRig.weight = Mathf.Lerp(headLookRig.weight, targetWeight, Time.deltaTime * rigWeightSpeed);
    }

    void LateUpdate()
    {

        dialogueCanvas.transform.forward = playerTarget.forward;

    }


    public void Interact()
    {
        string message = dialogue.RandomDialogues();

        PlayDialogue(message, lookDuration);
    }

    public void PlayDialogue(string text, float duration)
    {
        if (lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }
        lookCoroutine = StartCoroutine(LookAtPlayer(text, duration));
    }

    public void PlayDialogue(string text, float duration, float maxAngle)
    {
        if (lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }
        maxLookAngle = maxAngle;
        lookCoroutine = StartCoroutine(LookAtPlayer(text, duration));
    }

    private bool IsPlayerInSight()
    {
        if (playerTarget == null) return false;

        Vector3 forwardVector = transform.forward;
        Vector3 directionToPlayer = (playerTarget.position - transform.position).normalized;

        float angle = Vector3.Angle(forwardVector, directionToPlayer);

        return angle <= maxLookAngle;
    }

    IEnumerator LookAtPlayer(string message, float duration)
    {
        isLooking = true;
        if (dialogueCanvas != null && dialogueText != null)
        {
            dialogueCanvas.SetActive(true);
            dialogueText.text = message;
        }

        yield return new WaitForSeconds(duration);

        isLooking = false;
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }
    }

    private void OnEnable()
    {
        AnomalyManager.OnAnomalyHappened += InitCommunication;
    }

    private void OnDisable()
    {
        AnomalyManager.OnAnomalyHappened -= InitCommunication;
    }

    public void InitCommunication()
    {
        if (lookCoroutine != null)
        {
            StopCoroutine(lookCoroutine);
        }

        if (maxLookAngle > 120f)
        {
            maxLookAngle = 90f;
        }

        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }
    }
}
