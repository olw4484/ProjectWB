using UnityEngine;

public class WerebearIdleState : BaseEnemyState
{
    public WerebearIdleState(WerebearStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        animator.SetBool("IsMoving", false);
        Debug.Log("Entering Idle");
    }

    public override void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, stateMachine.Player.position);
        if (distanceToPlayer < 5f)
        {
            stateMachine.SetState(new WerebearChaseState(stateMachine));
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle");
    }
}
