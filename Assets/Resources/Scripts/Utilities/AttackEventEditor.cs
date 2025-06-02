#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class AttackEventEditor : EditorWindow
{
    private AnimationClip clip;
    private int hitboxIndex = 0;
    private float startTime = 0.5f;
    private float endTime = 0.7f;

    [MenuItem("Tools/Attack Event Inserter")]
    public static void ShowWindow()
    {
        GetWindow<AttackEventEditor>("Attack Event Inserter");
    }

    private void OnGUI()
    {
        GUILayout.Label("�ִϸ��̼� ���� �̺�Ʈ �ڵ� ���Ա�", EditorStyles.boldLabel);

        clip = (AnimationClip)EditorGUILayout.ObjectField("Animation Clip", clip, typeof(AnimationClip), false);
        hitboxIndex = EditorGUILayout.IntField("Hitbox Index", hitboxIndex);
        startTime = EditorGUILayout.FloatField("Start Time (sec)", startTime);
        endTime = EditorGUILayout.FloatField("End Time (sec)", endTime);

        if (clip == null)
        {
            EditorGUILayout.HelpBox("�ִϸ��̼� Ŭ���� �巡�����ּ���", MessageType.Warning);
            return;
        }

        if (GUILayout.Button("�̺�Ʈ ����"))
        {
            InsertEvents(clip, hitboxIndex, startTime, endTime);
        }
    }

    private void InsertEvents(AnimationClip targetClip, int index, float start, float end)
    {
        AnimationEvent startEvent = new AnimationEvent
        {
            functionName = "StartHitbox",
            time = start,
            intParameter = index
        };

        AnimationEvent endEvent = new AnimationEvent
        {
            functionName = "EndHitbox",
            time = end,
            intParameter = index
        };

        AnimationUtility.SetAnimationEvents(targetClip, new AnimationEvent[] { startEvent, endEvent });
        EditorUtility.SetDirty(targetClip);

        Debug.Log($"�̺�Ʈ ���� �Ϸ�: {targetClip.name} �� Hitbox {index} ({start:F2}s ~ {end:F2}s)");
    }
}
#endif
