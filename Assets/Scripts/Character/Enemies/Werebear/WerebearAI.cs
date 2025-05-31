using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class WerebearAI : MonoBehaviour, IHitReceiver
{

    private NavMeshAgent agent;
    private Animator animator;
    private WerebearStateMachine stateMachine;


    [Header("플레이어 추적")]
    [SerializeField] private Transform playerTarget;

    public Transform PlayerTarget => playerTarget;
    public NavMeshAgent Agent => agent;
    public Animator Animator => animator;


    public enum WerebearPhase
    {
        Phase1,
        Phase2,
        Phase3
    }

    [Header("페이즈 시스템")]
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private float currentHealth;
    [SerializeField] private WerebearPhase currentPhase = WerebearPhase.Phase1;

    public WerebearPhase CurrentPhase => currentPhase;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<WerebearStateMachine>();

        currentHealth = maxHealth;
    }

    private void Start()
    {

        stateMachine.Initialize(playerTarget, agent, animator);
    }

    private void Update()
    {

        CheckPhaseTransition();
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        Debug.Log($"[WerebearAI] 현재 체력: {currentHealth} / {maxHealth}");
    }


    private void CheckPhaseTransition()
    {
        if (currentPhase == WerebearPhase.Phase1 && currentHealth <= maxHealth * 0.7f)
        {
            SetPhase(WerebearPhase.Phase2);
        }
        else if (currentPhase == WerebearPhase.Phase2 && currentHealth <= maxHealth * 0.35f)
        {
            SetPhase(WerebearPhase.Phase3);
        }
    }


    private void SetPhase(WerebearPhase newPhase)
    {
        if (currentPhase == newPhase) return;

        currentPhase = newPhase;
        Debug.Log($"[WerebearAI] 페이즈 전환됨 {newPhase}");

        switch (newPhase)
        {
            case WerebearPhase.Phase2:
                agent.speed *= 1.1f;
                animator.SetTrigger("Roar");
                // TODO: 패턴 테이블 교체, 특수 공격 해금 등
                break;

            case WerebearPhase.Phase3:
                agent.speed *= 1.2f;
                animator.SetTrigger("RageRoar");
                // TODO: 연속 공격 루프, 경로 차단 AI 등
                break;
        }
    }

    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Debug.Log($"[WerebearAI] {damage} 피해를 받았습니다.");
    }

}
