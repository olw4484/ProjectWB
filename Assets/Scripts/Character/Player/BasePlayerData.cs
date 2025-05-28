using UnityEngine;

public class BasePlayerData : ScriptableObject
{
    [Header("기본 능력치")]
    public float maxHP = 100f;
    public float moveSpeed = 6f;
    public float dodgeCooldown = 1f;

    [Header("기타")]
    public float attackDamage = 10f;
    public float stamina = 100f;
}
