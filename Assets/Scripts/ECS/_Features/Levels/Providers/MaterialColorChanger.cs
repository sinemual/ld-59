using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialColorChanger : MonoBehaviour
{
    [System.Serializable]
    private class MaterialData
    {
        public Material material;
        public Color originalColor;
    }

    [SerializeField] private List<MaterialData> cachedMaterials = new List<MaterialData>();

#if UNITY_EDITOR
    [ContextMenu("Cache mats")]
    private void CacheMaterials()
    {
        cachedMaterials.Clear();
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
        {
            Material[] materials = renderer.materials; // Копируем материалы (создаются новые экземпляры!)
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].HasProperty("_BaseColor") && !cachedMaterials.Exists(m => m.material == materials[i]))
                {
                    cachedMaterials.Add(new MaterialData { material = materials[i], originalColor = materials[i].color });
                }
            }
        }

        EditorUtility.SetDirty(this); // Помечаем объект как измененный
    }
#endif

    public void ChangeColor(Color newColor)
    {
        foreach (var data in cachedMaterials)
            if (data.material != null)
                data.material.color = newColor;
    }

    public void RestoreOriginalColors()
    {
        foreach (var data in cachedMaterials)
            if (data.material != null)
                data.material.color = data.originalColor;
    }
}