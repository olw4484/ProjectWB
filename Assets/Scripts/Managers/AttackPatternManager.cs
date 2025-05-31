using System.Collections.Generic;
using UnityEngine;

public class AttackPatternManager : MonoBehaviour
{
    [Header("���� ���� ����Ʈ")]
    public List<AttackPatternSO> attackPatterns;

    [Header("��Ʈ�ڽ� Ž�� ���� ��Ʈ (�� ����)")]
    public Transform boneRoot;

    private int currentPatternIndex = -1;

    public void SetPattern(int index)
    {
        if (index < 0 || index >= attackPatterns.Count)
        {
            Debug.LogWarning($"[AttackPatternManager] �߸��� ���� �ε���: {index}");
            return;
        }

        currentPatternIndex = index;
    }

    public void ExecuteCurrentPattern()
    {
        if (currentPatternIndex < 0 || currentPatternIndex >= attackPatterns.Count)
        {
            Debug.LogWarning("[AttackPatternManager] ���� ���� �ε����� ��ȿ���� ����");
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
