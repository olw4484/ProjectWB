using UnityEngine;

public class WerebearDieState : BaseEnemyState
{
    public WerebearDieState(WerebearStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        animator.SetTrigger("DieTrigger");

        stateMachine.Agent.isStopped = true;
        stateMachine.Agent.enabled = false;

        MonoBehaviour.Destroy(stateMachine.GetComponent<Collider>()); // �ʿ� �� ����
        Debug.Log("Enemy Died");
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
