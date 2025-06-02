using UnityEngine;

public class WerebearHitState : BaseEnemyState
{
    private float staggerTime = 1.0f;
    private float timer = 0f;

    public WerebearHitState(WerebearStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        animator.SetBool("IsMoving", false);
        stateMachine.Agent.isStopped = true;

        int randomHit = Random.Range(1, 3);
        animator.Play($"Werebear_Hit0{randomHit}");

        timer = 0f;
        Debug.Log($"Hit Reaction: Hit0{randomHit}");
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer > staggerTime)
        {
            float distance = Vector3.Distance(transform.position, stateMachine.Player.position);

            if (distance > 5f)
                stateMachine.SetState(new WerebearChaseState(stateMachine));
            else
                stateMachine.SetState(new WerebearIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Agent.isStopped = false;
        Debug.Log("Exiting HitState");
    }
}
