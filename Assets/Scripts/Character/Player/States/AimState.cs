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
        if (!controller.IsAiming)
        {
            controller.SetState(new IdleState(controller));
            return;
        }

        if (controller.InputDir.magnitude > 0.1f)
        {
            controller.Move();
        }

        if (Input.GetMouseButtonDown(0)) // 좌클릭 시 발사 준비
        {
            controller.SetState(new DrawState(controller));
        }
    }

    public override void Exit()
    {
        controller.Animator.SetBool("IsAiming", false);
    }
}
