#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackPatternManager))]
public class AttackPatternManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AttackPatternManager manager = (AttackPatternManager)target;

        if (GUILayout.Button("SO 자동 등록"))
        {
            var patterns = Resources.LoadAll<AttackPatternSO>("");
            manager.attackPatterns.Clear();
            manager.attackPatterns.AddRange(patterns);
            EditorUtility.SetDirty(manager);
            Debug.Log($" {patterns.Length}개의 AttackPatternSO가 자동 등록되었습니다.");
        }
    }
}
#endif
