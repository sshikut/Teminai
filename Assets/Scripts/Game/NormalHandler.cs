using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class NormalHandler : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    public AnomalyManager anomaly;

    public TogglePhone toggle;

    public void NormalButton()
    {
        if (InteractionManager.Instance != null && InteractionManager.Instance.IsFading)
            return;

        AudioManager.instance.Play("Decision2");

        if (!anomaly.isAnomaly)
        {
            anomaly.loopCount++;
        }
        else
        {
            anomaly.absentCount++;
        }

        AudioManager.instance.Play("Decision2");

        InteractionManager.Instance.StartFadeOut();

        toggle.isActive = false;
        AudioManager.instance.Play("ClosePhone");
        toggle.anim.Play("Close");

        // 마우스 숨기고 고정 (FPS 스타일)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        firstPersonController.cameraRotation = true;

        anomaly.Anomaly();
       
    }
}
