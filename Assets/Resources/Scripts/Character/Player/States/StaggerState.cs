using UnityEngine;

public class StaggerState : BasePlayerState
{
    private float staggerDuration = 0.6f;
    private float timer = 0f;

    public StaggerState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: StaggerState");
        controller.Animator.SetTrigger("Hit");
        timer = 0f;

        // 무적 시간 부여 (선택)
        controller.SetInvincible(staggerDuration);
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= staggerDuration)
        {
            if (controller.IsAiming)
                controller.SetState(new AimState(controller));
            else
                controller.SetState(new IdleState(controller));
        }
    }

    public override void Exit()
    {
        // 필요 시 초기화 로직
    }
}
