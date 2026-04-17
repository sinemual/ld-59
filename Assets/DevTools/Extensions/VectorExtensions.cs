using UnityEngine;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 RandomPointOnLine(this Vector3 from, Vector3 to) =>
            Vector3.Lerp(from, to, Random.Range(0, 1f));

        public static Vector3 Set(this Vector3 v, float value)
        {
            v.x = value;
            v.y = value;
            v.z = value;
            return v;
        }

        public static Vector3 SetY(this Vector3 v, float y)
        {
            v.y = y;
            return v;
        }

        public static Vector3 SetX(this Vector3 v, float x)
        {
            v.x = x;
            return v;
        }

        public static Vector3 SetZ(this Vector3 v, float z)
        {
            v.z = z;
            return v;
        }

        public static bool IsInRangeXZ(this Vector3 v, Vector3 other, float range)
        {
            v.SetY(0);
            other.SetY(0);
            return Vector3.SqrMagnitude(v - other) <= range * range;
        }

        public static bool IsInRange(this Vector3 v, Vector3 other, float range) =>
            Vector3.SqrMagnitude(v - other) <= range * range;

        public static Vector3 GetRandomInRadiusXZ(this Vector3 v, float radius)
        {
            var r = Random.insideUnitCircle * radius;
            return new Vector3(r.x + v.x, v.y, r.y + v.z);
        }

        public static float GetRandom(this Vector2 vector) =>
            Random.Range(vector.x, vector.y);

        public static bool IsOnScreen(this Vector3 pos, Camera camera, Vector2 offset = default)
        {
            Vector3 screenPos = camera.WorldToViewportPoint(pos);
            return screenPos.x + offset.x >= 0 && screenPos.x - offset.x <= 1 && screenPos.y + offset.y >= 0 &&
                   screenPos.y - offset.y <= 1;
        }

        public static Vector3 MoveByDistanceTo(this Vector3 from, Vector3 to, float distance)
        {
            Vector3 direction = to - from;
            float sqrCurrentDistance = direction.sqrMagnitude;
            float sqrDistance = distance * distance;

            if (sqrCurrentDistance <= sqrDistance)
                return to;

            return from + direction.normalized * distance;
        }
    }
}