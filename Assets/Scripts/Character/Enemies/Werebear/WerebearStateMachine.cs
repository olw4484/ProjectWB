using UnityEngine;
using UnityEngine.AI;

public class WerebearStateMachine : MonoBehaviour
{
    private BaseEnemyState currentState;

    [field: SerializeField] public Animator Animator { get; private set; }
    public bool IsDead { get; private set; } = false;
    public Transform Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    public WerebearAI WerebearAI { get; private set; }

    public AttackPatternManager AttackManager { get; private set; }

    private void Awake()
    {
        AttackManager = GetComponent<AttackPatternManager>();
    }
    private void Start()
    {
        // 초기 상태는 Idle
        SetState(new WerebearIdleState(this));
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void SetState(BaseEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Initialize(Transform player, NavMeshAgent agent, Animator animator)
    {
        this.Player = player;
        this.Agent = agent;
        this.Animator = animator;
        this.WerebearAI = GetComponent<WerebearAI>();

        SetState(new WerebearIdleState(this));
    }

    public void Die()
    {
        if (IsDead) return;

        IsDead = true;
        SetState(new WerebearDieState(this));
    }
}
