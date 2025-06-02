using UnityEngine;

[System.Serializable]
public class HitboxEvent
{
    //public GameObject hitboxPrefab;         //���� ��Ʈ�ڽ� ��� ���
    public string attachBoneName;
    public string hitboxObjectName;
    public float startTime;
    public float endTime;

    [Range(0.1f, 5f)]
    public float damageMultiplier = 1.0f;
}
