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
        if (controller.InputDir.magnitude > 0.1f)
        {
            controller.Move();
        }
        else
        {
            controller.SetState(new IdleState(controller));
            return;
        }

        if (Input.GetMouseButtonDown(0)) // 좌클릭 시 발사 준비 상태 진입
        {
            controller.SetState(new DrawState(controller));
            return;
        }
    }

    public override void Exit()
    {
        // 필요시 조준 해제 처리
        controller.Animator.SetBool("IsAiming", false);
    }
}
