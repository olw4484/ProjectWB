using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackPattern", menuName = "Combat/Attack Pattern", order = 0)]
public class AttackPatternSO : ScriptableObject
{
    [Header("�⺻ ����")]
    public string patternName = "Phase2_DashAttack";
    public float damage = 25f;

    [Header("�ִϸ��̼� ����")]
    public AnimationClip animationClip;

    [Header("��Ʈ�ڽ� �̺�Ʈ")]
    public List<HitboxEvent> hitboxEvents;

    [Header("Ÿ�� ����Ʈ / ����")]
    public ParticleSystem hitEffect;
    public AudioClip hitSound;

    public void PlayEffect(Transform origin)
    {
        if (hitEffect != null)
            GameObject.Instantiate(hitEffect, origin.position, origin.rotation);

        if (hitSound != null)
        {
            var audio = origin.GetComponent<AudioSource>();
            if (audio != null)
                audio.PlayOneShot(hitSound);
        }
    }
}
