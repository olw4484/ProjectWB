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

        if (GUILayout.Button("SO �ڵ� ���"))
        {
            var patterns = Resources.LoadAll<AttackPatternSO>("");
            manager.attackPatterns.Clear();
            manager.attackPatterns.AddRange(patterns);
            EditorUtility.SetDirty(manager);
            Debug.Log($" {patterns.Length}���� AttackPatternSO�� �ڵ� ��ϵǾ����ϴ�.");
        }
    }
}
#endif
