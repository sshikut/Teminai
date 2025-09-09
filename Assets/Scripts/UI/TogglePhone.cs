using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    [SerializeField] private Animator anim; // �� ������Ʈ �Ҵ�
    [SerializeField] private GameObject phone; // �� ������Ʈ �Ҵ�
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
            phone.SetActive(isActive);

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