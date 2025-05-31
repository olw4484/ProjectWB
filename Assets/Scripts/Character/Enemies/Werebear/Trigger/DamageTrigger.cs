using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [Header("���� ����")]
    public float damage = 20f;
    public string targetTag = "Player";

    [Header("��ȸ�� Ÿ�� ����")]
    public bool deactivateAfterHit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        var receiver = other.GetComponent<IHitReceiver>();
        if (receiver != null)
        {
            receiver.TakeHit(damage, transform.position, transform.forward);
            Debug.Log($"{other.name}���� {damage} ���ظ� ����");
        }
        else
        {
            Debug.Log($"{other.name}��(��) �ǰ� ó���� �� ���� (IHitReceiver ����)");
        }

        if (deactivateAfterHit)
        {
            gameObject.SetActive(false);
        }
    }

}
