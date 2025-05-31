using UnityEngine;

public class DashAttackState : BaseEnemyState
{
    private WerebearAI werebear;
    private Animator _animator;
    private AttackPatternManager attackManager;

    public DashAttackState(WerebearAI werebear, Animator animator, AttackPatternManager attackManager, WerebearStateMachine stateMachine)
        : base(stateMachine)
    {
        this.werebear = werebear;
        this._animator = animator;
        this.attackManager = attackManager;
    }

    public override void Enter()
    {
        attackManager.SetPattern(0);
        _animator.SetTrigger("DashAttackTrigger");
    }

    public override void Update() { }

    public override void Exit()
    {
        foreach (var evt in attackManager.attackPatterns[0].hitboxEvents)
        {
            attackManager.EndHitbox(0);
        }
    }
}
