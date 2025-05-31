using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Cinemachine ī�޶� 2��")]
    [SerializeField] private CinemachineVirtualCamera followCam; // �⺻ TPS
    [SerializeField] private CinemachineVirtualCamera aimCam;    // ���� ����

    [Header("ī�޶� �Ÿ� ����")]
    [SerializeField] private float followDistance = 3.5f;
    [SerializeField] private float aimDistance = 2.0f;

    [Header("�ǹ� Ÿ��")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform aimTarget;

    [Header("������ ����")]
    [SerializeField] private Vector3 followOffset = new Vector3(0.6f, 2.0f, 0f);
    [SerializeField] private Vector3 aimOffset = new Vector3(0.3f, 1.6f, 0f);

    [Header("���� ����")]
    [SerializeField] private Vector3 followDamping = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private Vector3 aimDamping = new Vector3(0.05f, 0.05f, 0.05f);

    private Cinemachine3rdPersonFollow followComp;
    private Cinemachine3rdPersonFollow aimComp;

    private void Awake()
    {
        if (followCam != null)
            followComp = followCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (aimCam != null)
            aimComp = aimCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        Debug.Log("[CameraManager] Awake ���� Ȯ��");
    }

    public void SetAiming(bool isAiming)
    {
        // ī�޶� �켱���� ��ȯ
        followCam.Priority = isAiming ? 5 : 20;
        aimCam.Priority = isAiming ? 20 : 5;

        followCam.Follow = followTarget;
        followCam.LookAt = followTarget;

        aimCam.Follow = aimTarget;
        aimCam.LookAt = aimTarget;

        var brain = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
        if (brain != null && brain.ActiveVirtualCamera != null)
        {
            Debug.Log("[ī�޶� ����] ���� Ȱ�� ī�޶�: " + brain.ActiveVirtualCamera.Name);
        }

        // �� ����
        if (followComp != null)
        {
            followComp.CameraDistance = isAiming ? aimDistance : followDistance;
            followComp.ShoulderOffset = isAiming ? aimOffset : followOffset;
            followComp.Damping = isAiming ? aimDamping : followDamping;
        }

        if (aimComp != null)
        {
            aimComp.CameraDistance = aimDistance;
            aimComp.ShoulderOffset = aimOffset;
            aimComp.Damping = aimDamping;
        }
    }

}
