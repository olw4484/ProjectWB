using UnityEngine;

public class WerebearAttackState : BaseEnemyState
{
    private Transform player;
    private UnityEngine.AI.NavMeshAgent agent;

    private float attackCooldown = 1.5f;
    private float timer;
    private bool hasAttacked = false;

    public WerebearAttackState(WerebearStateMachine stateMachine) : base(stateMachine)
    {
        player = stateMachine.Player;
        agent = stateMachine.Agent;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        animator.SetBool("IsMoving", false);

        int randomAttack = Random.Range(0, 2);
        animator.SetInteger("AttackIndex", randomAttack);

        hasAttacked = true;
        timer = 0f;

        Debug.Log($"Entering Attack: Index {randomAttack}");
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        // 공격 쿨타임 후 상태 전환
        if (timer > attackCooldown)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > 3f)
                stateMachine.SetState(new WerebearChaseState(stateMachine));
            else
                stateMachine.SetState(new WerebearIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        animator.SetInteger("AttackIndex", -1); // 상태 초기화
        agent.isStopped = false;

        Debug.Log("Exiting Attack");
    }
}
