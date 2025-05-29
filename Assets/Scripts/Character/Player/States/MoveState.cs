using UnityEngine;

public class MoveState : BasePlayerState
{
    public MoveState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("[MoveState] 이동 상태 진입");
    }

    public override void Update()
    {
        // 공격 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            controller.SetState(new DrawState(controller));
            return;
        }

        if (controller.IsAiming)
        {
            controller.SetState(new AimState(controller));
            return;
        }

        controller.Move();

        if (controller.InputDir.magnitude < 0.1f)
        {
            controller.SetState(new IdleState(controller));
            return;
        }

        RotateTowardsMoveInput();
    }

    private void RotateTowardsMoveInput()
    {
        Vector3 moveDir = controller.MoveInput;
        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z));
            controller.transform.rotation = Quaternion.Slerp(
                controller.transform.rotation, targetRot, Time.deltaTime * 10f
            );
        }
    }
}
