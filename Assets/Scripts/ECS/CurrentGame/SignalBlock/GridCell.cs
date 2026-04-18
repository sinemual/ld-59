using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public struct GridCell
    {
        public int Id;
        public bool IsBusy;
        public Transform Point;
        public EcsEntity BlockEntity;
    }
}