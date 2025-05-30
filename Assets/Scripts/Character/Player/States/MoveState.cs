using UnityEngine;

public class MoveState : BasePlayerState
{
    public MoveState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: MoveState");
    }

    public override void Update()
    {
        if (controller.IsDead)
            return;

        if (controller.IsAiming)
        {
            controller.SetState(new AimState(controller));
            return;
        }

        if (controller.InputDir.magnitude < 0.1f)
        {
            controller.SetState(new IdleState(controller));
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.SetState(new DodgeState(controller));
            return;
        }

        controller.Move();
    }
}
