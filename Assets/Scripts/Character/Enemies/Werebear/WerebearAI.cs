using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class WerebearAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private WerebearStateMachine stateMachine;

    [Header("플레이어 추적")]
    [SerializeField] private Transform playerTarget;

    public Transform PlayerTarget => playerTarget;
    public NavMeshAgent Agent => agent;
    public Animator Animator => animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<WerebearStateMachine>();
    }

    private void Start()
    {
        // 플레이어 참조를 상태머신에 넘김
        stateMachine.Initialize(playerTarget, agent, animator);
    }
}
