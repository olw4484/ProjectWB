using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    [Header("���콺 ȸ�� �ΰ���")]
    [SerializeField] private float rotationSpeed = 2f;

    [Header("Y�� ȸ�� ���� (���� �þ�)")]
    [SerializeField] private float minY = -35f;
    [SerializeField] private float maxY = 70f;

    private float xRotation; // �¿� ȸ����
    private float yRotation; // ���� ȸ����

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        Vector3 angles = transform.localEulerAngles;
        xRotation = angles.y;
        yRotation = angles.x;
    }

    private void Update()
    {
        if (player == null || !player.IsAiming) return;

        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        xRotation += mouseX;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, minY, maxY);

        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);
    }
}
