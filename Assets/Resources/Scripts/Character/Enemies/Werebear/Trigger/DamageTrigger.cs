using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    [Header("공격 정보")]
    public float damage = 20f;
    public string targetTag = "Player";

    [Header("일회성 타격 여부")]
    public bool deactivateAfterHit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(targetTag)) return;

        var receiver = other.GetComponent<IHitReceiver>();
        if (receiver != null)
        {
            receiver.TakeHit(damage, transform.position, transform.forward);
            Debug.Log($"{other.name}에게 {damage} 피해를 입힘");
        }
        else
        {
            Debug.Log($"{other.name}은(는) 피격 처리할 수 없음 (IHitReceiver 없음)");
        }

        if (deactivateAfterHit)
        {
            gameObject.SetActive(false);
        }
    }

}
