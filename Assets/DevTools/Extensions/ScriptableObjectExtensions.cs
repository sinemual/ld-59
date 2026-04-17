using UnityEditor;
using UnityEngine;

namespace Extensions
{
    public static class ScriptableObjectExtensions
    {
#if UNITY_EDITOR
        public static void SaveAsset(this ScriptableObject so)
        {
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssetIfDirty(so);
        }
#endif
    }
}