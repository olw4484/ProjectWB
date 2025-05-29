using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    [Header("플레이어 공통 상태")]
    public float currentHP;
    public bool isDead;

    protected BasePlayerState currentState;

    public abstract Vector2 InputDir { get; }
    public abstract void Move();

    protected virtual void Update()
    {
        currentState?.Update();
    }

    protected virtual void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }

    protected virtual void Start()
    {
        isDead = false;
    }

    public void SetState(BasePlayerState newState)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
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
