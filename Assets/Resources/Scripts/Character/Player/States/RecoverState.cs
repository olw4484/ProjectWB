using UnityEngine;

public class RecoverState : BasePlayerState
{
    private float timer = 0f;
    private readonly float recoverDuration = 0.4f;

    public RecoverState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: RecoverState");
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
