using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static void SetPositionAndRotation(this Transform transform, Transform target) =>
            transform.SetPositionAndRotation(target.position, target.rotation);

        public static void DestroyAllChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}