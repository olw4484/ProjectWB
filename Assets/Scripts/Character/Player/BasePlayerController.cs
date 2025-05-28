using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    [Header("플레이어 공통 상태")]
    public float currentHP;
    public bool isDead;

    protected virtual void Start()
    {
        isDead = false;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHP -= amount;
        if (currentHP <= 0 && !isDead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} 사망");
    }
}
