using UnityEngine;

public class FireState : BasePlayerState
{
    private bool hasFired = false;
    private readonly int fireHash = Animator.StringToHash("Fire");

    public FireState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: FireState");
        controller.Animator.SetTrigger("Fire");
        hasFired = false;
    }

    public override void Update()
    {
        AnimatorStateInfo stateInfo = controller.Animator.GetCurrentAnimatorStateInfo(1); // UpperBody Layer
        Debug.Log($"[FireState] ���� ���� �ؽ�: {stateInfo.shortNameHash}");

        if (stateInfo.shortNameHash == fireHash)
        {
            if (!hasFired && stateInfo.normalizedTime >= 0.35f)
            {
                controller.FireArrow();
                hasFired = true;
            }

            if (stateInfo.normalizedTime >= 0.9f)
            {
                controller.SetState(new RecoverState(controller));
            }
        }
    }
}
