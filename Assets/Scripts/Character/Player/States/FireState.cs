using UnityEngine;

public class FireState : BasePlayerState
{
    public FireState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: FireState");

        controller.Animator.SetTrigger("Fire");

        FireArrow(); // ���� ȭ�� �߻�
    }

    private void FireArrow()
    {
        if (controller.FirePoint == null)
        {
            Debug.LogWarning("FirePoint�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ȭ�� �������� Resources �������� �ε��ϰų� ���� ������ �� ����
        GameObject arrowPrefab = Resources.Load<GameObject>("Arrow");

        if (arrowPrefab == null)
        {
            Debug.LogError("Arrow �������� Resources/Arrow�� �����ϴ�.");
            return;
        }

        GameObject arrow = Object.Instantiate(arrowPrefab, controller.FirePoint.position, controller.FirePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb?.AddForce(controller.FirePoint.forward * 30f, ForceMode.Impulse); // �� ����
    }

    public override void Update()
    {
        // �ĵ� ���� ��� RecoverState�� �ѱ� ���� ����
        controller.SetState(new RecoverState(controller));
    }
}
