using UnityEngine;
using UnityEngine.AI;

public class WerebearAttackState : BaseEnemyState
{
    private float timer = 0f;
    private float attackDelay = 1.5f;
    private bool hasAttacked = false;

    private NavMeshAgent agent;

    public WerebearAttackState(WerebearStateMachine stateMachine) : base(stateMachine)
    {
        agent = stateMachine.Agent;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        animator.SetBool("IsMoving", false);

        FacePlayer();

        var phase = stateMachine.WerebearAI.CurrentPhase;
        int randomAttack;

        if (phase == WerebearAI.WerebearPhase.Phase1)
        {
            randomAttack = Random.Range(0, 2); // 0 or 1
        }
        else if (phase == WerebearAI.WerebearPhase.Phase2)
        {
            randomAttack = Random.Range(0, 3); // 0~2
        }
        else
        {
            randomAttack = Phase3ComboStep(); // 0~3
        }

        // 애니메이션 이름으로 공격 패턴 설정
        string clipName = $"Werebear_Attack0{randomAttack + 1}"; // ex: Attack01 ~ Attack04
        stateMachine.AttackManager.SetPatternByName(clipName);

        animator.SetInteger("AttackIndex", randomAttack);
        hasAttacked = true;
        timer = 0f;

        Debug.Log($"[Attack] Phase: {phase}, Index: {randomAttack}, PatternName: {clipName}");
    }



    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackDelay)
        {
            float distance = Vector3.Distance(transform.position, stateMachine.Player.position);
            if (distance < 5f)
            {
                stateMachine.SetState(new WerebearAttackState(stateMachine));
            }
            else
            {
                stateMachine.SetState(new WerebearChaseState(stateMachine));
            }
        }
    }

    public override void Exit()
    {
        Debug.Log("[State] Exit Attack");
    }

    private void FacePlayer()
    {
        Vector3 direction = (stateMachine.Player.position - transform.position).normalized;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 360f * Time.deltaTime);
        }
    }

    private int Phase3ComboStep()
    {
      
        return Random.Range(0, 4);
    }
}
