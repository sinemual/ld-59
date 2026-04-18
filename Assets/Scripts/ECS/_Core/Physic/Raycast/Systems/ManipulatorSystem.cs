using Client.Data.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class ManipulatorSystem : IEcsRunSystem
    {
        private SharedData _data;

        private EcsFilter<LastMouseRaycast> _filter;
        private EcsFilter<GridProvider> _gridFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var raycastEvent = ref entity.Get<LastMouseRaycast>();

                _data.SceneData.Manipulator.transform.position = raycastEvent.HitPoint;
                
                foreach (var idz in _gridFilter)
                {
                    ref var gridEntity = ref _gridFilter.GetEntity(idz);
                    ref var grid = ref gridEntity.Get<GridProvider>();

                    Vector3 gridPosition = GetNearestGridCell(grid.GridCells, raycastEvent.HitPoint);
                    _data.SceneData.CellManipulator.transform.position = gridPosition + Vector3.up * 0.5f;
                }
            }
        }

        private Vector3 GetNearestGridCell(Transform[] gridCells, Vector3 manipulatorPosition)
        {
            float nearestDistance = 1000.0f;
            int nearestGridCellNum = 0;
            for (int i = 0; i < gridCells.Length; i++)
            {
                var distance = Vector3.Distance(gridCells[i].position, manipulatorPosition);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestGridCellNum = i;
                }
            }
            
            return gridCells[nearestGridCellNum].position;
        }
    }
}