using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotherTrigger : MonoBehaviour
{
    public FirstPersonController player;
    public Communication student;

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

        Quaternion targetCapsuleRotation = Quaternion.Euler(0, 90f, 0);
        Quaternion targetCameraRotation = Quaternion.Euler(0, 0, 0);

        while (elapsedTime < lookDuration)
        {
            playerCapsule.rotation = Quaternion.Slerp(startCapsuleRotation, targetCapsuleRotation, elapsedTime / lookDuration);
            playerCamera.localRotation = Quaternion.Slerp(startCameraRotation, targetCameraRotation, elapsedTime / lookDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }


        playerCapsule.rotation = targetCapsuleRotation;
        playerCamera.localRotation = targetCameraRotation;

        student.PlayDialogue("반대님 시험 언제에요?", 30f);

        this.enabled = false;
    }
}
