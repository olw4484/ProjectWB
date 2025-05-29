using UnityEngine;

public class StaggerState : BasePlayerState
{
    private float staggerDuration = 0.4f;
    private float timer = 0f;

    public StaggerState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: StaggerState");
        timer = 0f;
        controller.Animator.SetTrigger("Hit");
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= staggerDuration)
        {
            if (controller.IsAiming)
                controller.SetState(new AimState(controller));
            else if (controller.InputDir.magnitude > 0.1f)
                controller.SetState(new MoveState(controller));
            else
                controller.SetState(new IdleState(controller));
        }
    }
}
