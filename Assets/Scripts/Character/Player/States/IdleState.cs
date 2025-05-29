using UnityEngine;

public class IdleState : BasePlayerState
{
    public IdleState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: IdleState");
        controller.Animator.SetBool("IsAiming", false);
    }

    public override void Update()
    {
        // ������ �߻� �� MoveState�� ����
        if (controller.InputDir.magnitude > 0.1f)
        {
            controller.SetState(new MoveState(controller));
            return;
        }

        // ���� ���� ����
        if (controller.IsAiming)
        {
            controller.SetState(new AimState(controller));
        }
    }


    public override void Exit()
    {
        
    }
}

