using Client.Data.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class ManipulatorSystem : IEcsRunSystem
    {
        private SharedData _data;

        private EcsFilter<LastMouseRaycast> _raycastFilter;
        private EcsFilter<GridCell> _gridFilter;
        private EcsFilter<ManipulatorProvider> _manipulatorFilter;

        public void Run()
        {
            foreach (var idm in _manipulatorFilter)
            {
                ref var manipulatorEntity = ref _manipulatorFilter.GetEntity(idm);
                ref var manipulator = ref manipulatorEntity.Get<ManipulatorProvider>();
                ref var manipulatorGo = ref manipulatorEntity.Get<GameObjectProvider>().Value;

                foreach (var idx in _raycastFilter)
                {
                    ref var entity = ref _raycastFilter.GetEntity(idx);
                    ref var raycastEvent = ref entity.Get<LastMouseRaycast>();

                    manipulatorGo.transform.position = raycastEvent.HitPoint;

                    if (manipulatorGo.transform.position.x >= _data.SceneData.GridZoneStart.position.x &&
                        manipulatorGo.transform.position.x <= _data.SceneData.GridZoneEnd.position.x &&
                        manipulatorGo.transform.position.z >= _data.SceneData.GridZoneStart.position.z &&
                        manipulatorGo.transform.position.z <= _data.SceneData.GridZoneEnd.position.z)
                        manipulatorEntity.Get<InGridState>();
                    else
                    {
                        if (manipulatorEntity.Has<InGridState>())
                            manipulatorEntity.Del<InGridState>();
                    }

                    if (manipulatorEntity.Has<InGridState>())
                    {
                        float nearestDistance = 1000.0f;
                        int nearestGridCellNum = 0;
                        Vector3 gridCellPosition = Vector3.zero;
                        foreach (var idz in _gridFilter)
                        {
                            ref var gridEntity = ref _gridFilter.GetEntity(idz);
                            ref var gridCell = ref gridEntity.Get<GridCell>();

                            //Debug.Log($"gridCell.Point {gridCell.Point}");
                            //Debug.Log($"raycastEvent.HitPoint {raycastEvent.HitPoint}");
                            var distance = Vector3.Distance(gridCell.Point.position, raycastEvent.HitPoint);
                            if (distance < nearestDistance)
                            {
                                nearestDistance = distance;
                                nearestGridCellNum = gridCell.Id;
                                gridCellPosition = gridCell.Point.position;
                            }
                        }
                        
                        var gridCellNum = nearestGridCellNum;
                        manipulatorEntity.Get<InGridState>().GridCellNum = gridCellNum;
                        Vector3 gridPosition = gridCellPosition;
                        manipulatorGo.transform.position = gridPosition + Vector3.up * 0.5f;
                        /*Vector3.Lerp(manipulatorGo.transform.position, gridPosition + Vector3.up * 0.5f,
                                10 * Time.deltaTime);*/
                    }
                }
            }
        }
    }

    public struct InGridState
    {
        public int GridCellNum;
    }
}