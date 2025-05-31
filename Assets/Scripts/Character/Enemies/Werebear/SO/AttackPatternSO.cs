using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttackPattern", menuName = "Combat/Attack Pattern", order = 0)]
public class AttackPatternSO : ScriptableObject
{
    [Header("기본 정보")]
    public string patternName = "Phase2_DashAttack";
    public float damage = 25f;

    [Header("애니메이션 연결")]
    public AnimationClip animationClip;

    [Header("히트박스 이벤트")]
    public List<HitboxEvent> hitboxEvents;

    [Header("타격 이펙트 / 사운드")]
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
