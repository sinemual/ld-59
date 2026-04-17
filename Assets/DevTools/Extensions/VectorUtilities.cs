using UnityEngine;

namespace Extensions
{
    public static class VectorUtilities
    {
        public static Vector3 Create(float value) =>
            new(value, value, value);

        public static Vector3 CreateX(Vector3 origin, float x) =>
            new(x, origin.y, origin.z);

        public static Vector3 CreateY(Vector3 origin, float y) =>
            new(origin.x, y, origin.z);

        public static Vector3 RandomBetweenAxes(Vector3 a, Vector3 b) =>
            new(
                Random.Range(a.x, b.x),
                Random.Range(a.y, b.y),
                Random.Range(a.z, b.z)
            );
    }
}