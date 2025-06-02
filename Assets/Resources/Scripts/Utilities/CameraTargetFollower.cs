using UnityEngine;

public class CameraTargetFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 20f;
    [SerializeField] private bool followRotation = false;

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);

        if (followRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * followSpeed);
        }
    }
}
