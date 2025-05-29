using UnityEngine;

public class FireState : BasePlayerState
{
    public FireState(PlayerController controller) : base(controller) { }

    public override void Enter()
    {
        Debug.Log("상태 전환: FireState");

        controller.Animator.SetTrigger("Fire");

        FireArrow(); // 실제 화살 발사
    }

    private void FireArrow()
    {
        if (controller.FirePoint == null)
        {
            Debug.LogWarning("FirePoint가 설정되지 않았습니다.");
            return;
        }

        // 화살 프리팹은 Resources 폴더에서 로드하거나 직접 연결할 수 있음
        GameObject arrowPrefab = Resources.Load<GameObject>("Arrow");

        if (arrowPrefab == null)
        {
            Debug.LogError("Arrow 프리팹이 Resources/Arrow에 없습니다.");
            return;
        }

        GameObject arrow = Object.Instantiate(arrowPrefab, controller.FirePoint.position, controller.FirePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb?.AddForce(controller.FirePoint.forward * 30f, ForceMode.Impulse); // 힘 조절
    }

    public override void Update()
    {
        // 후딜 없이 즉시 RecoverState로 넘길 수도 있음
        controller.SetState(new RecoverState(controller));
    }
}
