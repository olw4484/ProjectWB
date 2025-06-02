using UnityEngine;

public class DieState : BasePlayerState
{
    public DieState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: DieState");

        // ��� �ִϸ��̼� Ʈ����
        controller.Animator.SetTrigger("Die");

        // ��� ��ġ �ٴ����� ���� ����
        RaycastHit hit;
        Vector3 origin = controller.transform.position + Vector3.up;
        if (Physics.Raycast(origin, Vector3.down, out hit, 2f))
        {
            Vector3 grounded = controller.transform.position;
            grounded.y = hit.point.y;
            controller.transform.position = grounded;
        }

        // controller.enabled = false;
    }

    public override void Update()
    {
        // ���� ���¿����� �Է��̳� �ൿ ����
    }

    public override void Exit()
    {
        // ��Ȱ ���� ó�� �ʿ�� ���⿡ �ۼ�
    }
}
