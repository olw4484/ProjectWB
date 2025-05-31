using UnityEngine;

public class AttackHitboxManager : MonoBehaviour
{
    [Header("���ط� ����")]
    [SerializeField] private float damage = 20f;

    [Header("���� ��Ʈ�ڽ�")]
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

    // �޼� ���� ����/����
    public void OnLeftArmAttackStart() => leftArmHitbox.enabled = true;
    public void OnLeftArmAttackEnd() => leftArmHitbox.enabled = false;

    // ������ ���� ����/����
    public void OnRightArmAttackStart() => rightArmHitbox.enabled = true;
    public void OnRightArmAttackEnd() => rightArmHitbox.enabled = false;
}
