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

        // 플레이어 자식에 달린 카메라 찾기
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
        {
            Debug.LogError("자식 오브젝트에 Camera가 없습니다! Player 오브젝트 밑에 카메라를 넣어주세요.");
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
