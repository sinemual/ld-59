using Client.Data.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class MouseRaycastSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;

        private EcsFilter<MouseRaycastProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var raycastProvider = ref entity.Get<MouseRaycastProvider>();
                var mousePosition = Input.mousePosition;
                //mousePosition.z = _data.SceneData.CameraSceneData.MainCamera.nearClipPlane;
                var ray = _data.SceneData.CameraSceneData.MainCamera.ScreenPointToRay(mousePosition);
                if (Physics.Raycast(ray, out var hit, raycastProvider.RaycastLength, raycastProvider.PlacementLayerMask))
                {
                    entity.Get<LastMouseRaycast>() = new LastMouseRaycast
                    {
                        GameObject = hit.collider.gameObject,
                        HitPoint = hit.point
                    };
                }
            }
        }
    }
    
    public struct LastMouseRaycast
    {
        public GameObject GameObject;
        public Vector3 HitPoint;
    }
}