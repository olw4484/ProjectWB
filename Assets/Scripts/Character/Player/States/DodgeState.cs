using UnityEngine;

public class DodgeState : BasePlayerState
{
    private float dodgeDuration = 0.7f;
    private float timer = 0f;

    public DodgeState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: DodgeState");

        timer = 0f;
        controller.Animator.SetTrigger("Dodge");

        // 강제로 바닥에 붙이기 위한 지면 보정
        RaycastHit hit;
        Vector3 origin = controller.transform.position + Vector3.up;
        if (Physics.Raycast(origin, Vector3.down, out hit, 2f))
        {
            Vector3 groundedPosition = controller.transform.position;
            groundedPosition.y = hit.point.y;
            controller.CharacterController.enabled = false; // 위치 강제 조정시 잠깐 비활성화 필요
            controller.transform.position = groundedPosition;
            controller.CharacterController.enabled = true;
        }
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= dodgeDuration)
        {
            if (controller.IsAiming)
                controller.SetState(new AimState(controller));
            else
                controller.SetState(new IdleState(controller));
        }
    }

    public override void Exit()
    {
        // 필요시 애니메이터 관련 초기화 로직 추가 가능
    }
}
