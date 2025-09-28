using StarterAssets;
using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    public FirstPersonController firstPersonController;

    [SerializeField] public Animator anim; // 폰 애니메이터
    [SerializeField] public Animator anim2; // 폰 애니메이터

    public bool isActive = false;
    private bool canToggle = true;

    private bool phone1 = false;
    private bool phone2 = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canToggle && !phone1)
        {
            phone2 = !phone2;
            isActive = !isActive;

            if (isActive)
            {
                AudioManager.instance.Play("OpenPhone");
                anim.Play("Open");

                // 마우스 보이게
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                firstPersonController.cameraRotation = false;
            }
            else
            {
                AudioManager.instance.Play("ClosePhone");
                anim.Play("Close");

                // 마우스 숨기고 고정 (FPS 스타일)
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                firstPersonController.cameraRotation = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canToggle && !phone2)
        {
            phone1 = !phone1;
            isActive = !isActive;

            if (isActive)
            {
                AudioManager.instance.Play("OpenPhone");
                anim2.Play("Open");

                // 마우스 보이게
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                firstPersonController.cameraRotation = false;
            }
            else
            {
                AudioManager.instance.Play("ClosePhone");
                anim2.Play("Close");

                // 마우스 숨기고 고정 (FPS 스타일)
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                firstPersonController.cameraRotation = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
            {
            DisableToggleAfterOneUse();
        }
    }

    public void Exit()
    {
        Debug.Log("게임 종료 요청됨");
        Application.Quit();
    }

    public void DisableToggleAfterOneUse()
    {

        //isActive = true;
        //AudioManager.instance.Play("OpenPhone");
        //anim.Play("Open");
        //canToggle = false;
        if (isActive == false)  // ui가 열려있으면 안함
        {
            isActive = !isActive; // ui 열기
            AudioManager.instance.Play("OpenPhone");
            anim.Play("Open");
        }

        // EndGame 이후 딱 한 번만 → 커서도 열릴 때처럼 보이게
        Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            firstPersonController.cameraRotation = false;
        
    }
}