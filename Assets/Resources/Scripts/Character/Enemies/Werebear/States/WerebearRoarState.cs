using UnityEngine;

public class WerebearRoarState : BaseEnemyState
{
    private float roarDuration = 2.5f;
    private float timer;

    public WerebearRoarState(WerebearStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        timer = 0f;
        animator.SetTrigger("RoarTrigger");
        stateMachine.Agent.isStopped = true;

        Debug.Log("[State] Roar 상태 진입");

        if (stateMachine.WerebearAI.CurrentPhase == WerebearAI.WerebearPhase.Phase2)
        {
            if (stateMachine.Player.TryGetComponent<IStaggerable>(out var staggerable))
            {
                staggerable.Stagger(1.0f);
                Debug.Log("[State] 플레이어 경직 유발");
            }
        }
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > roarDuration)
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
        Debug.Log("[State] Roar 상태 종료");
        stateMachine.Agent.isStopped = false;
    }
}
