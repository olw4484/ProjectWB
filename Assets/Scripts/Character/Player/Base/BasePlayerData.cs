using UnityEngine;

public class BasePlayerData : ScriptableObject
{
    [Header("�⺻ �ɷ�ġ")]
    public float maxHP = 100f;
    public float moveSpeed = 2.5f;
    public float dodgeCooldown = 1f;

    [Header("��Ÿ")]
    public float attackDamage = 10f;
    public float stamina = 100f;
}
