using UnityEngine;

public class DodgeState : BasePlayerState
{
    private float dodgeDuration = 0.6f;
    private float timer = 0f;

    public DodgeState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: DodgeState");
        timer = 0f;
        controller.Animator.SetTrigger("Dodge");
        controller.SetInvincible(dodgeDuration); // ���� �ð� �ο�
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dodgeDuration)
        {
            if (controller.IsAiming)
                controller.SetState(new AimState(controller));
            else if (controller.InputDir.magnitude > 0.1f)
                controller.SetState(new MoveState(controller));
            else
                controller.SetState(new IdleState(controller));
        }
    }
}
