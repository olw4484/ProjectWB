using UnityEngine;

public class DodgeState : BasePlayerState
{
    private float dodgeDuration = 0.7f;
    private float timer = 0f;

    public DodgeState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: DodgeState");

        timer = 0f;
        controller.Animator.SetTrigger("Dodge");

        // ������ �ٴڿ� ���̱� ���� ���� ����
        RaycastHit hit;
        Vector3 origin = controller.transform.position + Vector3.up;
        if (Physics.Raycast(origin, Vector3.down, out hit, 2f))
        {
            Vector3 groundedPosition = controller.transform.position;
            groundedPosition.y = hit.point.y;
            controller.CharacterController.enabled = false; // ��ġ ���� ������ ��� ��Ȱ��ȭ �ʿ�
            controller.transform.position = groundedPosition;
            controller.CharacterController.enabled = true;
        }
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dodgeDuration)
        {
            if (controller.IsAiming)
                controller.SetState(new AimState(controller));
            else
                controller.SetState(new IdleState(controller));
        }
    }

    public override void Exit()
    {
        // �ʿ�� �ִϸ����� ���� �ʱ�ȭ ���� �߰� ����
    }
}
