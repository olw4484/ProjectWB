using UnityEngine;

public class AimState : BasePlayerState
{
    public AimState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: AimState");
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

        if (Input.GetMouseButtonDown(0)) // ��Ŭ�� �� �߻� �غ� ���� ����
        {
            controller.SetState(new DrawState(controller));
            return;
        }
    }

    public override void Exit()
    {
        // �ʿ�� ���� ���� ó��
        controller.Animator.SetBool("IsAiming", false);
    }
}
