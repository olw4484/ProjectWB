using UnityEngine;

public class HitState : BasePlayerState
{
    private bool hasReacted = false;

    public HitState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: HitState");

        controller.Animator.SetTrigger("Hit");
        hasReacted = false;
    }

    public override void Update()
    {
        AnimatorStateInfo stateInfo = controller.Animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Hit"))
        {
            if (!hasReacted && stateInfo.normalizedTime >= 0.1f)
            {
                hasReacted = true;
            }

            if (stateInfo.normalizedTime >= 0.95f)
            {
                if (controller.IsAiming)
                    controller.SetState(new AimState(controller));
                else
                    controller.SetState(new IdleState(controller));
            }
        }
    }

    public override void Exit()
    {
        controller.Animator.ResetTrigger("Hit");
    }
}
