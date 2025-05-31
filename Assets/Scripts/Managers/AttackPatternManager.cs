using System.Collections.Generic;
using UnityEngine;

public class AttackPatternManager : MonoBehaviour
{
    [Header("공격 패턴 리스트")]
    public List<AttackPatternSO> attackPatterns;

    [Header("히트박스 탐색 기준 루트 (본 구조)")]
    public Transform boneRoot;

    private int currentPatternIndex = -1;

    public void SetPattern(int index)
    {
        if (index < 0 || index >= attackPatterns.Count)
        {
            Debug.LogWarning($"[AttackPatternManager] 잘못된 패턴 인덱스: {index}");
            return;
        }

        currentPatternIndex = index;
    }

    public void ExecuteCurrentPattern()
    {
        if (currentPatternIndex < 0 || currentPatternIndex >= attackPatterns.Count)
        {
            Debug.LogWarning("[AttackPatternManager] 현재 패턴 인덱스가 유효하지 않음");
            return;
        }

        attackPatterns[currentPatternIndex].PlayEffect(boneRoot != null ? boneRoot : transform);
    }

    public void StartHitbox(int index)
    {
        var pattern = attackPatterns[currentPatternIndex];
        if (index < 0 || index >= pattern.hitboxEvents.Count) return;

        var evt = pattern.hitboxEvents[index];
        var bone = boneRoot.Find(evt.attachBoneName);
        if (bone != null)
        {
            var hitbox = bone.Find(evt.hitboxObjectName);
            if (hitbox != null)
                hitbox.gameObject.SetActive(true);
        }
    }

    public void EndHitbox(int index)
    {
        var pattern = attackPatterns[currentPatternIndex];
        if (index < 0 || index >= pattern.hitboxEvents.Count) return;

        var evt = pattern.hitboxEvents[index];
        var bone = boneRoot.Find(evt.attachBoneName);
        if (bone != null)
        {
            var hitbox = bone.Find(evt.hitboxObjectName);
            if (hitbox != null)
                hitbox.gameObject.SetActive(false);
        }
    }
}
