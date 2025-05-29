using UnityEngine;

public class DrawState : BasePlayerState
{
    private float drawDuration = 0.5f; // Ȱ ���� �ð�
    private float timer = 0f;

    public DrawState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("���� ��ȯ: DrawState");
        controller.Animator.SetTrigger("Draw"); // �ִϸ��̼� Ʈ����
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
        // ���߿� �ִϸ��̼� ���� �� ó�� ����
    }
}
