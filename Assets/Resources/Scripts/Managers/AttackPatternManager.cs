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
            Debug.LogWarning($"[AttackPatternManager] '{patternName}' �̸��� ���� ������ ã�� �� �����ϴ�.");
        }
    }

    public void ExecuteCurrentPattern()
    {
        if (currentPattern == null)
        {
            Debug.LogWarning("[AttackPatternManager] ���� ���õ� ���� ������ �����ϴ�.");
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
