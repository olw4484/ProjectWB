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
        GUILayout.Label("SO 기반 애니메이션 이벤트 자동 삽입기", EditorStyles.boldLabel);

        attackSO = (AttackPatternSO)EditorGUILayout.ObjectField("AttackPatternSO", attackSO, typeof(AttackPatternSO), false);

        if (attackSO == null || attackSO.animationClip == null)
        {
            EditorGUILayout.HelpBox("SO와 AnimationClip을 지정해주세요", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("이벤트 자동 삽입"))
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

        Debug.Log($"{clip.name} 애니메이션에 {so.hitboxEvents.Count}개의 히트박스 이벤트가 삽입되었습니다.");
    }
}
#endif
