using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    [SerializeField] private Animator anim; // 폰 오브젝트 할당
    [SerializeField] private GameObject phone; // 폰 오브젝트 할당

    private bool isActive = false;
    private bool canToggle = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
            // phone.SetActive(isActive);

            if (isActive)
            {
                GameManager.Instance.audioManager.Play("OpenPhone");
                anim.Play("Open");
            }
            else 
            {
                GameManager.Instance.audioManager.Play("ClosePhone");
                anim.Play("Close");
            }
        }
    }
    public void DisableToggleAfterOneUse()
    {
        isActive = !isActive;
        GameManager.Instance.audioManager.Play("OpenPhone");
        anim.Play("Open");
        canToggle = false; // EndGame 이후 딱 한 번만 토글 가능하도록 허용
    }
}