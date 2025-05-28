using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("기본 스탯")]
    public float maxHP = 100f;
    public float currentHP;

    public float maxStamina = 100f;
    public float currentStamina;

    [Header("기타 수치")]
    public float moveSpeed = 5f;
    public float dodgeCost = 20f;
    public float staminaRegenRate = 10f;

    void Awake()
    {
        currentHP = maxHP;
        currentStamina = maxStamina;
    }

    void Update()
    {
        RegenerateStamina();
    }

    private void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }
    }

    public bool CanDodge()
    {
        return currentStamina >= dodgeCost;
    }

    public void UseStamina(float amount)
    {
        currentStamina = Mathf.Max(0, currentStamina - amount);
    }

    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Max(0, currentHP - damage);
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망");
        GameManager.Instance.SetState(GameManager.GameState.GameOver);
    }
}
