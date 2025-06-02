using UnityEngine;

[CreateAssetMenu(menuName = "Character/Player/MorganaData")]
public class MorganaData : BasePlayerData
{
    [Header("모르가나 전용")]
    public float drawTime = 0.5f; // 활을 당기는 시간
    public float fireDelay = 0.2f; // 발사 후 딜레이
    public float aimZoomFOV = 40f; // 조준 시 카메라 시야각

    [Header("활 관련")]
    public GameObject arrowPrefab;
    public Transform firePoint;
    public float arrowSpeed = 25f;
}
