using StarterAssets;
using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    [SerializeField] public Animator anim;  // Tab(Phone2)
    [SerializeField] public Animator anim2; // Esc(Phone1)

    public bool isActive = false;
    private bool canToggle = true;

    public bool phone1 = false; // Esc
    public bool phone2 = false; // Tab

    void Update()
    {
        // ───────────────── Tab → Phone2 ─────────────────
        if (Input.GetKeyDown(KeyCode.Tab) && canToggle && !phone1)
        {
            if (!phone2)
            {
                // ★ 열기 전에 다른 UI가 열려 있는지 체크
                if (UIGuard.isAnyUIOpen) return;

                phone2 = true;
                isActive = true;
                UIGuard.isAnyUIOpen = true; // ★ 점유 시작

                if (AudioManager.instance) AudioManager.instance.Play("OpenPhone");
                if (anim) anim.Play("Open");

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (firstPersonController) firstPersonController.cameraRotation = false;
            }
            else
            {
                // 닫기는 항상 허용
                phone2 = false;
                isActive = phone1 || phone2;
                UIGuard.isAnyUIOpen = false; // ★ 점유 해제(단순 버전)

                if (AudioManager.instance) AudioManager.instance.Play("ClosePhone");
                if (anim) anim.Play("Close");

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if (firstPersonController) firstPersonController.cameraRotation = true;
            }
        }

        // ───────────────── Esc → Phone1 ─────────────────
        if (Input.GetKeyDown(KeyCode.Escape) && canToggle && !phone2)
        {
            if (!phone1)
            {
                // ★ 열기 전에 다른 UI가 열려 있는지 체크
                if (UIGuard.isAnyUIOpen) return;

                phone1 = true;
                isActive = true;
                UIGuard.isAnyUIOpen = true; // ★ 점유 시작

                if (AudioManager.instance) AudioManager.instance.Play("OpenPhone");
                if (anim2) anim2.Play("Open");

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (firstPersonController) firstPersonController.cameraRotation = false;
            }
            else
            {
                // 닫기는 항상 허용
                phone1 = false;
                isActive = phone1 || phone2;
                UIGuard.isAnyUIOpen = false; // ★ 점유 해제(단순 버전)

                if (AudioManager.instance) AudioManager.instance.Play("ClosePhone");
                if (anim2) anim2.Play("Close");

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if (firstPersonController) firstPersonController.cameraRotation = true;
            }
        }
    }

    public void Exit()
    {
        Debug.Log("게임 종료 요청됨");
        Application.Quit();
    }

    // ★ 초간단 전역 가드(여기 두거나 별도 파일로 빼도 됨)
    public static class UIGuard
    {
        public static bool isAnyUIOpen = false;
    }
}