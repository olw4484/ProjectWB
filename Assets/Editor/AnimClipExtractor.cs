using UnityEditor;
using UnityEngine;

public class AnimClipExtractor : MonoBehaviour
{
    [MenuItem("Tools/Extract AnimationClips from FBX")]
    static void ExtractClips()
    {
        var selected = Selection.activeObject as GameObject;
        if (!selected) return;

        var path = AssetDatabase.GetAssetPath(selected);
        var assets = AssetDatabase.LoadAllAssetsAtPath(path);

        foreach (var asset in assets)
        {
            if (asset is AnimationClip clip && !AssetDatabase.IsSubAsset(asset))
                continue;

            var newClip = Object.Instantiate(asset) as AnimationClip;
            if (newClip == null) continue;

            var newPath = "Assets/Animations/Extracted/" + asset.name + ".anim";
            AssetDatabase.CreateAsset(newClip, newPath);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("AnimationClip extraction complete.");
    }
}
