using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 4.0f;

    [Header("Mouse Look")]
    [SerializeField] private float lookSensitivity = 2.0f;
    [SerializeField] private float cameraRotationLimit = 80f;

    private Rigidbody rb;
    private Camera cam;
    private float currentCameraRotationX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // �÷��̾� �ڽĿ� �޸� ī�޶� ã��
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
        {
            Debug.LogError("�ڽ� ������Ʈ�� Camera�� �����ϴ�! Player ������Ʈ �ؿ� ī�޶� �־��ּ���.");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        CharacterRotation();
        CameraRotation();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");   // A/D
        float z = Input.GetAxisRaw("Vertical");     // W/S

        Vector3 move = (transform.right * x + transform.forward * z).normalized * moveSpeed;
        rb.MovePosition(rb.position + move * Time.deltaTime);
    }

    private void CharacterRotation()
    {
        float yRot = Input.GetAxisRaw("Mouse X") * lookSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, yRot, 0f));
    }

    private void CameraRotation()
    {
        if (cam == null) return;

        float xRot = Input.GetAxisRaw("Mouse Y") * lookSensitivity;
        currentCameraRotationX -= xRot;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
