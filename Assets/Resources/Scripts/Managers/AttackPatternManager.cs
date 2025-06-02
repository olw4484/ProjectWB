using System.Collections.Generic;
using UnityEngine;

public class AttackPatternManager : MonoBehaviour
{
    public List<AttackPatternSO> attackPatterns;
    public Transform boneRoot;

    private AttackPatternSO currentPattern;

    public void SetPatternByName(string patternName)
    {
        currentPattern = attackPatterns.Find(p => p.patternName == patternName);

        if (currentPattern == null)
        {
            Debug.LogWarning($"[AttackPatternManager] '{patternName}' 이름의 공격 패턴을 찾을 수 없습니다.");
        }
    }

    public void ExecuteCurrentPattern()
    {
        if (currentPattern == null)
        {
            Debug.LogWarning("[AttackPatternManager] 현재 선택된 공격 패턴이 없습니다.");
            return;
        }

        currentPattern.PlayEffect(boneRoot != null ? boneRoot : transform);
    }

    public void StartHitbox(int index)
    {
        if (currentPattern == null || index >= currentPattern.hitboxEvents.Count) return;

        var evt = currentPattern.hitboxEvents[index];
        var bone = boneRoot.Find(evt.attachBoneName);
        var hitbox = bone?.Find(evt.hitboxObjectName);
        hitbox?.gameObject.SetActive(true);
    }

    public void EndHitbox(int index)
    {
        if (currentPattern == null || index >= currentPattern.hitboxEvents.Count) return;

        var evt = currentPattern.hitboxEvents[index];
        var bone = boneRoot.Find(evt.attachBoneName);
        var hitbox = bone?.Find(evt.hitboxObjectName);
        hitbox?.gameObject.SetActive(false);
    }
}
