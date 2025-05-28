using UnityEngine;

[CreateAssetMenu(menuName = "Character/Player/MorganaData")]
public class MorganaData : BasePlayerData
{
    [Header("�𸣰��� ����")]
    public GameObject arrowPrefab;
    public float drawTime = 0.5f; // Ȱ�� ���� �ð�
    public float fireDelay = 0.2f; // �߻� �� ������
    public float aimZoomFOV = 40f; // ���� �� ī�޶� �þ߰�
}
