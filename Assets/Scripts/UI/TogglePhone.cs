using UnityEngine;

public class TogglePhone : MonoBehaviour
{
    [SerializeField] private GameObject phone; // �� ������Ʈ �Ҵ�
    private bool isActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isActive = !isActive;
            phone.SetActive(isActive);
        }
    }
}