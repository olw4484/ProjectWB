using UnityEngine;

public class DashAttackState : BaseEnemyState
{
    private WerebearAI werebear;
    private Animator _animator;
    private AttackPatternManager attackManager;
    private string patternName;

    public DashAttackState(WerebearAI werebear, Animator animator, AttackPatternManager attackManager, WerebearStateMachine stateMachine, string patternName)
        : base(stateMachine)
    {
        this.werebear = werebear;
        this._animator = animator;
        this.attackManager = attackManager;
        this.patternName = patternName;
    }

    public override void Enter()
    {
        attackManager.SetPatternByName(patternName);
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
