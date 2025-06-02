
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [Header("�ó׸ӽ� ī�޶�")]
    [SerializeField] private CinemachineVirtualCamera followCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;

    [Header("�÷��̾� ����")]
    [SerializeField] private PlayerStatus playerStatus;

    [Header("�켱���� ����")]
    [SerializeField] private int activePriority = 20;
    [SerializeField] private int inactivePriority = 10;

    private void Start()
    {
        if (playerStatus == null)
        {
            Debug.LogError("PlayerStatus�� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }

        playerStatus.IsAiming.Subscribe(OnAimingChanged);
        OnAimingChanged(playerStatus.IsAiming.Value); // ���� �� �ʱ�ȭ
    }

    private void OnAimingChanged(bool isAiming)
    {
        followCamera.Priority = isAiming ? inactivePriority : activePriority;
        aimCamera.Priority = isAiming ? activePriority : inactivePriority;
    }
}
