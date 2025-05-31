using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("Cinemachine 카메라 2대")]
    [SerializeField] private CinemachineVirtualCamera followCam; // 기본 TPS
    [SerializeField] private CinemachineVirtualCamera aimCam;    // 에임 시점

    [Header("카메라 거리 설정")]
    [SerializeField] private float followDistance = 3.5f;
    [SerializeField] private float aimDistance = 2.0f;

    [Header("피벗 타겟")]
    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform aimTarget;

    [Header("오프셋 설정")]
    [SerializeField] private Vector3 followOffset = new Vector3(0.6f, 2.0f, 0f);
    [SerializeField] private Vector3 aimOffset = new Vector3(0.3f, 1.6f, 0f);

    [Header("댐핑 설정")]
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

        Debug.Log("[CameraManager] Awake 진입 확인");
    }

    public void SetAiming(bool isAiming)
    {
        // 카메라 우선순위 전환
        followCam.Priority = isAiming ? 5 : 20;
        aimCam.Priority = isAiming ? 20 : 5;

        followCam.Follow = followTarget;
        followCam.LookAt = followTarget;

        aimCam.Follow = aimTarget;
        aimCam.LookAt = aimTarget;

        var brain = Camera.main.GetComponent<Cinemachine.CinemachineBrain>();
        if (brain != null && brain.ActiveVirtualCamera != null)
        {
            Debug.Log("[카메라 상태] 현재 활성 카메라: " + brain.ActiveVirtualCamera.Name);
        }

        // 값 적용
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
