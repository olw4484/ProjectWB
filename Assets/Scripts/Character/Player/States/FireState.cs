using UnityEngine;

public class FireState : BasePlayerState
{
    private bool hasFired = false;

    public FireState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: FireState");

        controller.Animator.SetTrigger("Fire");
        hasFired = false;
    }

    public override void Update()
    {
        AnimatorStateInfo stateInfo = controller.Animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Fire"))
        {
            if (!hasFired && stateInfo.normalizedTime >= 0.35f)
            {
                FireArrow();
                hasFired = true;
            }

            if (stateInfo.normalizedTime >= 0.9f)
            {
                controller.SetState(new RecoverState(controller));
            }
        }
    }

    private void FireArrow()
    {
        if (controller.FirePoint == null || controller.ArrowPrefab == null)
        {
            Debug.LogWarning("FirePoint �Ǵ� ArrowPrefab�� �������� �ʾҽ��ϴ�.");
            return;
        }

        GameObject arrow = Object.Instantiate(controller.ArrowPrefab, controller.FirePoint.position, controller.FirePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb?.AddForce(controller.FirePoint.forward * 30f, ForceMode.Impulse);
    }
}
