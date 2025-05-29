using UnityEngine;

public class IdleState : BasePlayerState
{
    public IdleState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: IdleState");
        controller.Animator.SetBool("IsAiming", false);
    }

    public override void Update()
    {
        // 움직임 발생 시 MoveState로 전이
        if (controller.InputDir.magnitude > 0.1f)
        {
            controller.SetState(new MoveState(controller));
            return;
        }

        // 조준 상태 전이
        if (controller.IsAiming)
        {
            controller.SetState(new AimState(controller));
        }
    }


    public override void Exit()
    {
        
    }
}

