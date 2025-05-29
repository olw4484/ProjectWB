using UnityEngine;

public class DrawState : BasePlayerState
{
    private float drawDuration = 0.5f; // 활 당기기 시간
    private float timer = 0f;

    public DrawState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: DrawState");
        controller.Animator.SetTrigger("Draw"); // 애니메이션 트리거
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= drawDuration)
        {
            controller.SetState(new FireState(controller));
        }
    }

    public override void Exit()
    {
        // 나중에 애니메이션 리셋 등 처리 가능
    }
}
