using Client.Data.Core;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace Data.Base
{
    public abstract class BaseDataSO : ScriptableObject
    {
        protected SharedData SharedData;

        public virtual void ResetData()
        {
        }

#if UNITY_EDITOR
        [Button]
        public new void SetDirty()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }
}