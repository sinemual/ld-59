using UnityEngine;

namespace Extensions
{
    public static class QuaternionExtensions
    {
        public static Quaternion SmoothDamp(this Quaternion current, Quaternion target, ref Vector3 currentVelocity, float smoothTime)
        {
            Vector3 c = current.eulerAngles;
            Vector3 t = target.eulerAngles;
            return Quaternion.Euler(
                Mathf.SmoothDampAngle(c.x, t.x, ref currentVelocity.x, smoothTime),
                Mathf.SmoothDampAngle(c.y, t.y, ref currentVelocity.y, smoothTime),
                Mathf.SmoothDampAngle(c.z, t.z, ref currentVelocity.z, smoothTime)
            );
        }

        public static bool IsEqualTo(this Quaternion a, Quaternion b) =>
            Quaternion.Angle(a, b) == 0f;
    }
}