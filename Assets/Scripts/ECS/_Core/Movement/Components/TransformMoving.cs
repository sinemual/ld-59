using UnityEngine;

namespace Client
{
    internal struct TransformMoving
    {
        public Transform Target;
        public Vector3 TargetPosition;
        public float Speed;
        public float Accuracy;
    }
    
    internal struct TransformAroundMoving
    {
        public Transform Target;
        public Vector3 TargetPosition;
        public float Speed;
        public float Accuracy;
        public float Radius;
        public Vector3 Offset;
    }
}