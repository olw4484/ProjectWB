
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("시네머신 카메라")]
    [SerializeField] private CinemachineVirtualCamera followCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;

    [Header("플레이어 상태")]
    [SerializeField] private PlayerStatus playerStatus;

    [Header("우선순위 설정")]
    [SerializeField] private int activePriority = 20;
    [SerializeField] private int inactivePriority = 10;

    private void Start()
    {
        if (playerStatus == null)
        {
            Debug.LogError("PlayerStatus가 할당되지 않았습니다.");
            return;
        }

        playerStatus.IsAiming.Subscribe(OnAimingChanged);
        OnAimingChanged(playerStatus.IsAiming.Value); // 시작 시 초기화
    }

    private void OnAimingChanged(bool isAiming)
    {
        followCamera.Priority = isAiming ? inactivePriority : activePriority;
        aimCamera.Priority = isAiming ? activePriority : inactivePriority;
    }
}
