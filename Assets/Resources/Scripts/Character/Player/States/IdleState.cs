using UnityEngine;

public class IdleState : BasePlayerState
{
    public IdleState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: IdleState");
        controller.Animator.SetFloat("MoveSpeed", 0f);
    }

    public override void Update()
    {
        if (controller.IsAiming)
        {
            controller.SetState(new AimState(controller));
            return;
        }

        if (controller.InputDir.magnitude > 0.1f)
        {
            controller.SetState(new MoveState(controller));
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.SetState(new DodgeState(controller));
            return;
        }
    }
}
