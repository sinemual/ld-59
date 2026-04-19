using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class SpawnFirstBlockSystem : IEcsRunSystem
    {
        private PrefabFactory _factory;
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        private PrefabFactory _prefabFactory;
        private CameraService _cameraService;
        private AudioService _audioService;

        private EcsFilter<SpawnFirstBlockRequest> _filter;
        private EcsFilter<GridCell> _gridFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var levelProvider = ref entity.Get<LevelProvider>();

                var blockData = _data.StaticData.GetBlockByParameters(SignalBlockType.OneSignal, InputDirection.Back);
                EcsEntity blockEntity = _prefabFactory.Spawn(
                    blockData.Prefab,
                    _data.SceneData.StartBlockPoint.position, Quaternion.identity);
                
                blockEntity.Get<SignalBlockDataComponent>().Value = blockData;
                
                float nearestDistance = 1000.0f;
                int nearestGridCellNum = 0;
                Vector3 gridCellPosition = Vector3.zero;
                foreach (var idz in _gridFilter)
                {
                    ref var gridEntity = ref _gridFilter.GetEntity(idz);
                    ref var gridCell = ref gridEntity.Get<GridCell>();

                    var distance = Vector3.Distance(gridCell.Point.position, blockEntity.Get<GameObjectProvider>().Value.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestGridCellNum = gridCell.Id;
                        gridCellPosition = gridCell.Point.position;
                    }
                }

                foreach (var idz in _gridFilter)
                {
                    ref var gridEntity = ref _gridFilter.GetEntity(idz);
                    ref var gridCell = ref gridEntity.Get<GridCell>();

                    if (gridCell.Id == nearestGridCellNum)
                    {
                        blockEntity.Get<GameObjectProvider>().Value.transform.position = gridCellPosition + Vector3.up * 0.5f;
                        blockEntity.Get<MyGrid>().Value = gridEntity;
                        gridCell.IsBusy = true;
                        gridCell.BlockEntity = blockEntity;
                    }
                }

                entity.Del<SpawnFirstBlockRequest>();
            }
        }
    }
}