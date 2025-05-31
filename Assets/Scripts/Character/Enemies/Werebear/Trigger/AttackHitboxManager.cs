using UnityEngine;

public class AttackHitboxManager : MonoBehaviour
{
    [Header("피해량 설정")]
    [SerializeField] private float damage = 20f;

    [Header("공격 히트박스")]
    [SerializeField] private Collider leftArmHitbox;
    [SerializeField] private Collider rightArmHitbox;

    private void Awake()
    {
        leftArmHitbox.enabled = false;
        rightArmHitbox.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var receiver = other.GetComponent<IHitReceiver>();
        if (receiver != null)
        {
            receiver.TakeHit(damage, transform.position, transform.forward);
        }
    }

    // 왼손 공격 시작/종료
    public void OnLeftArmAttackStart() => leftArmHitbox.enabled = true;
    public void OnLeftArmAttackEnd() => leftArmHitbox.enabled = false;

    // 오른손 공격 시작/종료
    public void OnRightArmAttackStart() => rightArmHitbox.enabled = true;
    public void OnRightArmAttackEnd() => rightArmHitbox.enabled = false;
}
