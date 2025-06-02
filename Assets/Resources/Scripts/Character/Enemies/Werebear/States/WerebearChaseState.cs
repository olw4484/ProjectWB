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

        if (stateMachine.WerebearAI.CurrentPhase == WerebearAI.WerebearPhase.Phase2)
            agent.speed *= 1.25f;

        else if (stateMachine.WerebearAI.CurrentPhase == WerebearAI.WerebearPhase.Phase3)
            agent.speed *= 1.4f;

        agent.updateRotation = false;
        Debug.Log("Entering Chase");
    }

    public override void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        agent.SetDestination(player.position);

        if (agent.desiredVelocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(agent.desiredVelocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
        }

        // 이동 애니메이션 보간
        float speed = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("MoveSpeed", speed, 0.2f, Time.deltaTime);

        // 상태 전이
        var phase = stateMachine.WerebearAI.CurrentPhase;

        if (phase >= WerebearAI.WerebearPhase.Phase2 && distance < 6f)
        {
            stateMachine.SetState(new DashAttackState(stateMachine.WerebearAI, stateMachine.Animator, stateMachine.AttackManager, stateMachine, "Werebear_Dash_Attack1"));
            return;
        }

        if (distance < attackRange)
        {
            stateMachine.SetState(new WerebearAttackState(stateMachine));
            return;
        }

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

        agent.speed = stateMachine.WerebearAI.Agent.speed;

        Debug.Log("Exiting Chase");
    }
}
