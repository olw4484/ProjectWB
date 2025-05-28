using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [Header("Cinemachine Virtual Cameras")]
    public CinemachineVirtualCamera baseCamera;
    public CinemachineVirtualCamera aimCamera;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        HandleCameraSwitch();
    }

    private void HandleCameraSwitch()
    {
        if (Input.GetMouseButton(1)) // 우클릭 조준 중
        {
            baseCamera.Priority = 0;
            aimCamera.Priority = 10;
        }
        else
        {
            baseCamera.Priority = 10;
            aimCamera.Priority = 0;
        }
    }
}
