using UnityEngine;

public abstract class BasePlayerController : MonoBehaviour
{
    public event System.Action OnDeath;
    public event System.Action OnRevive;

    [Header("플레이어 공통 상태")]
    [SerializeField] private float maxHP = 100f;
    public float currentHP { get; private set; }

    protected bool isDead = false;
    public bool IsDead => isDead;

    protected BasePlayerState currentState;

    public BasePlayerState CurrentState => currentState;

    protected virtual void Start()
    {
        currentHP = maxHP;
        isDead = false;
    }

    protected virtual void Update()
    {
        if (!isDead)
            currentState?.Update();
    }

    protected virtual void FixedUpdate()
    {
        if (!isDead)
            currentState?.FixedUpdate();
    }

    public void SetState(BasePlayerState newState)
    {
        if (isDead) return;

        if (currentState != null)
            currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHP -= amount;
        if (currentHP <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log($"{gameObject.name} 사망");
        OnDeath?.Invoke();
    }


    public virtual void Revive(float hp)
    {
        currentHP = hp;
        isDead = false;
        OnRevive?.Invoke();
    }

    public abstract Vector2 InputDir { get; }
    public abstract void Move();
}
