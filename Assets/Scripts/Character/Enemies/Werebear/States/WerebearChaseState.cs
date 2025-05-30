using UnityEngine;

public class WerebearChaseState : BaseEnemyState
{
    private Transform player;
    private UnityEngine.AI.NavMeshAgent agent;
    private float chaseRange = 10f;
    private float attackRange = 3f;

    public WerebearChaseState(WerebearStateMachine stateMachine) : base(stateMachine)
    {
        player = stateMachine.Player;
        agent = stateMachine.Agent;

    }

    public override void Enter()
    {
        animator.SetBool("IsMoving", true);
        agent.isStopped = false;
        Debug.Log("Entering Chase");
        agent.updateRotation = false;
    }

    public override void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 목적지 지정
        agent.SetDestination(player.position);

        float speed = agent.velocity.magnitude / agent.speed; // 0~1 정규화
        animator.SetFloat("MoveSpeed", speed, 0.2f, Time.deltaTime); // Damping 적용

        // 공격 사거리 도달 시 전이
        if (distance < attackRange)
        {
            stateMachine.SetState(new WerebearAttackState(stateMachine));
            return;
        }

        // 추적 범위 벗어나면 Idle로 전환
        if (distance > chaseRange)
        {
            stateMachine.SetState(new WerebearIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        animator.SetBool("IsMoving", false);
        animator.SetFloat("MoveSpeed", 0f);
        agent.isStopped = true;
        Debug.Log("Exiting Chase");
    }
}
