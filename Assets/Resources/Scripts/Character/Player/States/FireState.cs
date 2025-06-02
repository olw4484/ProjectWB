using UnityEngine;

public class FireState : BasePlayerState
{
    private bool hasFired = false;
    private readonly int fireHash = Animator.StringToHash("Fire");

    public FireState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: FireState");
        controller.Animator.SetTrigger("Fire");
        hasFired = false;
    }

    public override void Update()
    {
        AnimatorStateInfo stateInfo = controller.Animator.GetCurrentAnimatorStateInfo(1); // UpperBody Layer
        Debug.Log($"[FireState] 현재 상태 해시: {stateInfo.shortNameHash}");

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
