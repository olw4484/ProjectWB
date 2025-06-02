#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class AttackEventBatchInserter : EditorWindow
{
    private AttackPatternSO attackSO;

    [MenuItem("Tools/Batch Insert Attack Events")]
    public static void ShowWindow()
    {
        GetWindow<AttackEventBatchInserter>("Attack Event Inserter");
    }

    private void OnGUI()
    {
        GUILayout.Label("SO ��� �ִϸ��̼� �̺�Ʈ �ڵ� ���Ա�", EditorStyles.boldLabel);

        attackSO = (AttackPatternSO)EditorGUILayout.ObjectField("AttackPatternSO", attackSO, typeof(AttackPatternSO), false);

        if (attackSO == null || attackSO.animationClip == null)
        {
            EditorGUILayout.HelpBox("SO�� AnimationClip�� �������ּ���", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("�̺�Ʈ �ڵ� ����"))
        {
            InsertEvents(attackSO);
        }
    }

    private void InsertEvents(AttackPatternSO so)
    {
        var clip = so.animationClip;
        var events = new AnimationEvent[so.hitboxEvents.Count * 2];

        for (int i = 0; i < so.hitboxEvents.Count; i++)
        {
            var evt = so.hitboxEvents[i];

            AnimationEvent startEvt = new AnimationEvent
            {
                functionName = "StartHitbox",
                time = evt.startTime,
                intParameter = i
            };

            AnimationEvent endEvt = new AnimationEvent
            {
                functionName = "EndHitbox",
                time = evt.endTime,
                intParameter = i
            };

            events[i * 2] = startEvt;
            events[i * 2 + 1] = endEvt;
        }

        AnimationUtility.SetAnimationEvents(clip, events);
        EditorUtility.SetDirty(clip);

        Debug.Log($"{clip.name} �ִϸ��̼ǿ� {so.hitboxEvents.Count}���� ��Ʈ�ڽ� �̺�Ʈ�� ���ԵǾ����ϴ�.");
    }
}
#endif
