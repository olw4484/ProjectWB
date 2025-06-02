using UnityEngine;

public class DrawState : BasePlayerState
{

    public DrawState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: DrawState");
        controller.Animator.Play("Draw", 1, 0f);
    }

    public override void Update()
    {
        AnimatorStateInfo stateInfo = controller.Animator.GetCurrentAnimatorStateInfo(1);

        if (controller.Animator.GetLayerWeight(1) >= 0.99f && stateInfo.IsName("Draw"))
        {
            if (stateInfo.normalizedTime >= 0.9f)
            {
                Debug.Log("Draw 애니메이션 종료, FireState로 전환");
                controller.SetState(new FireState(controller));
            }
        }
    }

}
