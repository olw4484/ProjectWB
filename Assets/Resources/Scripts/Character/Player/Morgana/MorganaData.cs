using UnityEngine;

[CreateAssetMenu(menuName = "Character/Player/MorganaData")]
public class MorganaData : BasePlayerData
{
    [Header("�𸣰��� ����")]
    public float drawTime = 0.5f; // Ȱ�� ���� �ð�
    public float fireDelay = 0.2f; // �߻� �� ������
    public float aimZoomFOV = 40f; // ���� �� ī�޶� �þ߰�

    [Header("Ȱ ����")]
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float arrowSpeed = 25f;
}
