using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotherTrigger : MonoBehaviour
{
    public FirstPersonController player;
    public Communication student;
    public RapidPressQTE qte;

    public Transform playerCapsule;
    public Transform playerCamera;

    private Coroutine lookCoroutine;

    public float lookDuration = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 이전에 실행 중이던 코루틴이 있다면 중지
            if (lookCoroutine != null)
            {
                StopCoroutine(lookCoroutine);
            }
            // 새로운 시선 고정 코루틴 시작
            lookCoroutine = StartCoroutine(SmoothLookAtCoroutine());
        }
    }

    private IEnumerator SmoothLookAtCoroutine()
    {
        player.lockMovement = true;
        player.cameraRotation = false;

        float elapsedTime = 0f;

        Quaternion startCapsuleRotation = playerCapsule.rotation;
        Quaternion startCameraRotation = playerCamera.localRotation;
        Vector3 startPosition = playerCapsule.position;

        Quaternion targetCapsuleRotation = Quaternion.Euler(0, 90f, 0);
        Quaternion targetCameraRotation = Quaternion.Euler(0, 0, 0);
        Vector3 targetPosition = new Vector3(14f, startPosition.y, startPosition.z);

        while (elapsedTime < lookDuration)
        {
            float t = elapsedTime / lookDuration;

            playerCapsule.rotation = Quaternion.Slerp(startCapsuleRotation, targetCapsuleRotation, t);
            playerCamera.localRotation = Quaternion.Slerp(startCameraRotation, targetCameraRotation, t);

            Vector3 currentFrameTargetPosition = Vector3.Lerp(startPosition, targetPosition, t);
            Vector3 movementDelta = currentFrameTargetPosition - playerCapsule.position;

            player.forcedMovementDelta = movementDelta;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCapsule.rotation = targetCapsuleRotation;
        playerCamera.localRotation = targetCameraRotation;

        Vector3 finalMovementDelta = targetPosition - playerCapsule.position;
        player.forcedMovementDelta = finalMovementDelta;

        student.PlayDialogue("아까 교수님이 뭐라고 말씀하신거에요?", 30f);
        qte.StartQTE();

        this.enabled = false;
    }
}
