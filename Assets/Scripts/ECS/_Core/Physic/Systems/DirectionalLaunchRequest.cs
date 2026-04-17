using UnityEngine;

namespace Client
{
    internal struct DirectionalLaunchRequest
    {
        public Vector3 LaunchPosition;
        public Vector2 Force;
        public Vector3 Direction;
    }
}