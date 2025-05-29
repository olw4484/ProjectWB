using UnityEngine;

public class AimState : BasePlayerState
{
    public AimState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: AimState");
        controller.Animator.SetBool("IsAiming", true);
    }

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            controller.SetState(new DrawState(controller));
            return;
        }

        if (controller.InputDir.magnitude > 0.1f)
        {
            controller.Move();
        }
        else
        {
            controller.SetState(new IdleState(controller));
            return;
        }
    }


    public override void Exit()
    {
        // 필요시 조준 해제 처리
        controller.Animator.SetBool("IsAiming", false);
    }
}
