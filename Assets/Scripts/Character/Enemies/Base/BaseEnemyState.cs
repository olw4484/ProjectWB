
using UnityEngine;

public abstract class BaseEnemyState
{
    protected WerebearStateMachine stateMachine;
    protected Animator animator;
    protected Transform transform;

    public BaseEnemyState(WerebearStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.animator = stateMachine.Animator;
        this.transform = stateMachine.transform;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
