#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class CopyComponentsTool
{
    [MenuItem("Tools/Copy Components/Copy From Selected")]
    public static void CopyFromSelected()
    {
        if (Selection.gameObjects.Length != 2)
        {
            Debug.LogError("Select EXACTLY two GameObjects: source first, then target.");
            return;
        }

        GameObject source = Selection.gameObjects[0];
        GameObject target = Selection.gameObjects[1];

        CopyAllComponents(source, target);
    }

    public static void CopyAllComponents(GameObject source, GameObject target)
    {
        Component[] components = source.GetComponents<Component>();

        foreach (var comp in components)
        {
            if (comp is Transform) continue;

            System.Type type = comp.GetType();
            Component copy = target.AddComponent(type);

            // копируем поля и свойства
            CopyComponentData(comp, copy);
        }

        Debug.Log($"Copied all components from {source.name} → {target.name}");
    }

    private static void CopyComponentData(Component source, Component target)
    {
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        // Копируем поля
        FieldInfo[] fields = source.GetType().GetFields(flags);
        foreach (var f in fields)
        {
            if (f.IsDefined(typeof(SerializeField), true) || f.IsPublic)
            {
                f.SetValue(target, f.GetValue(source));
            }
        }

        // Копируем свойства (которые можно записать)
        PropertyInfo[] props = source.GetType().GetProperties(flags);
        foreach (var p in props)
        {
            if (!p.CanWrite || !p.CanRead) continue;
            if (p.SetMethod == null) continue;     // нет сеттера
            if (p.GetMethod == null) continue;     // нет геттера
            if (p.GetIndexParameters().Length > 0) continue; // исключаем индексаторы

            try
            {
                p.SetValue(target, p.GetValue(source));
            }
            catch { } // пропускаем то, что Unity не позволяет писать
        }
    }
}
#endif