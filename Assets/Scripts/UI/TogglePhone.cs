using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    [SerializeField] public Animator anim; // 폰 애니메이터
    [SerializeField] private GameObject phone; // 폰 오브젝트

    public bool isActive = false;
    private bool canToggle = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canToggle)
        {
            isActive = !isActive;

            if (isActive)
            {
                AudioManager.instance.Play("OpenPhone");
                anim.Play("Open");

                // 마우스 보이게
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                AudioManager.instance.Play("ClosePhone");
                anim.Play("Close");

                // 마우스 숨기고 고정 (FPS 스타일)
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void DisableToggleAfterOneUse()
    {
        isActive = true;
        AudioManager.instance.Play("OpenPhone");
        anim.Play("Open");
        canToggle = false;

        // EndGame 이후 딱 한 번만 → 커서도 열릴 때처럼 보이게
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}