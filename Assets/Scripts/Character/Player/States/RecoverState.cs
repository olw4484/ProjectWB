using UnityEngine;

public class RecoverState : BasePlayerState
{
    private float recoverDuration = 0.4f;
    private float timer = 0f;

    public RecoverState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: RecoverState");
        timer = 0f;
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= recoverDuration)
        {
            if (controller.IsAiming)
                controller.SetState(new AimState(controller));
            else
                controller.SetState(new IdleState(controller));
        }
    }
}
