using UnityEngine;

public class DieState : BasePlayerState
{
    public DieState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: DieState");

        // 사망 애니메이션 트리거
        controller.Animator.SetTrigger("Die");

        // 사망 위치 바닥으로 강제 보정
        RaycastHit hit;
        Vector3 origin = controller.transform.position + Vector3.up;
        if (Physics.Raycast(origin, Vector3.down, out hit, 2f))
        {
            Vector3 grounded = controller.transform.position;
            grounded.y = hit.point.y;
            controller.transform.position = grounded;
        }

        // controller.enabled = false;
    }

    public override void Update()
    {
        // 죽은 상태에서는 입력이나 행동 없음
    }

    public override void Exit()
    {
        // 부활 관련 처리 필요시 여기에 작성
    }
}
