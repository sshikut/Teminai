using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    [SerializeField] private Animator anim; // 폰 오브젝트 할당
    [SerializeField] private GameObject phone; // 폰 오브젝트 할당
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;

            if (isActive)
            {
                anim.Play("Open");
            }
            else 
            {
                anim.Play("Close");
            }
            
        }
    }
}