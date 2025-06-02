using UnityEngine;

public class DrawState : BasePlayerState
{
    public DrawState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: DrawState");
        controller.Animator.SetTrigger("Draw");
    }

    public override void Update()
    {
        AnimatorStateInfo stateInfo = controller.Animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Draw"))
        {
            if (stateInfo.normalizedTime >= 0.9f)
            {
                controller.SetState(new FireState(controller));
            }
        }
    }
}
