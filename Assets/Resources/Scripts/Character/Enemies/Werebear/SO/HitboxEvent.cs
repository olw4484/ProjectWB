using UnityEngine;

[System.Serializable]
public class HitboxEvent
{
    //public GameObject hitboxPrefab;         //동적 히트박스 사용 고려
    public string attachBoneName;
    public string hitboxObjectName;
    public float startTime;
    public float endTime;

    [Range(0.1f, 5f)]
    public float damageMultiplier = 1.0f;
}
