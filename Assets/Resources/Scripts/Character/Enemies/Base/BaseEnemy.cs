using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHitReceiver
{
    [Header("공통 스탯")]
    public float maxHP = 100f;
    protected float currentHP;

    [Header("컴포넌트")]
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log($"[{gameObject.name}] 피격: {amount} → 잔여 HP: {currentHP}");

        if (currentHP <= 0f)
        {
            Die();
        }
        else
        {
            OnHit(); // 경직, 반응 애니메이션 등
        }
    }

    protected virtual void OnHit()
    {
        // 히트 리액션 (애니메이션 등) 구현
        animator?.SetTrigger("Hit");
    }

    protected virtual void Die()
    {
        Debug.Log($"[{gameObject.name}] 사망");
        animator?.SetTrigger("Die");

        // 사망 후 처리를 위해 일정 시간 뒤 제거
        Destroy(gameObject, 3f);
    }

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
    }
}
