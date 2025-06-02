using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour, IHitReceiver
{
    [Header("���� ����")]
    public float maxHP = 100f;
    protected float currentHP;

    [Header("������Ʈ")]
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log($"[{gameObject.name}] �ǰ�: {amount} �� �ܿ� HP: {currentHP}");

        if (currentHP <= 0f)
        {
            Die();
        }
        else
        {
            OnHit(); // ����, ���� �ִϸ��̼� ��
        }
    }

    protected virtual void OnHit()
    {
        // ��Ʈ ���׼� (�ִϸ��̼� ��) ����
        animator?.SetTrigger("Hit");
    }

    protected virtual void Die()
    {
        Debug.Log($"[{gameObject.name}] ���");
        animator?.SetTrigger("Die");

        // ��� �� ó���� ���� ���� �ð� �� ����
        Destroy(gameObject, 3f);
    }

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
    }
}
