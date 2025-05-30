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
        Debug.Log("Entering Roar");
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > roarDuration)
        {
            float distance = Vector3.Distance(transform.position, stateMachine.Player.position);

            if (distance < 5f)
                stateMachine.SetState(new WerebearAttackState(stateMachine));
            else
                stateMachine.SetState(new WerebearChaseState(stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Roar");
    }
}
