using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera followCamera;
    [SerializeField] private CinemachineVirtualCamera aimCamera;

    [SerializeField] private int activePriority = 20;
    [SerializeField] private int inactivePriority = 10;

    public void SetAiming(bool isAiming)
    {
        followCamera.Priority = isAiming ? inactivePriority : activePriority;
        aimCamera.Priority = isAiming ? activePriority : inactivePriority;
    }
}
