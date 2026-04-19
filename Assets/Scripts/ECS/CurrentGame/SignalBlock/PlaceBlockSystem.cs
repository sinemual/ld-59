using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class PlaceBlockSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<TapRaycastEvent> _tapRequestFilter;
        private EcsFilter<ManipulatorProvider, IsHaveBlockState, InGridState> _manipulatorFilter;
        private EcsFilter<GridCell> _gridFilter;

        public void Run()
        {
            foreach (var idx in _tapRequestFilter)
            {
                ref var requestEntity = ref _tapRequestFilter.GetEntity(idx);
                ref var request = ref requestEntity.Get<TapRaycastEvent>();

                foreach (var idz in _manipulatorFilter)
                {
                    ref var manipulatorEntity = ref _manipulatorFilter.GetEntity(idz);
                    ref var manipulator = ref manipulatorEntity.Get<ManipulatorProvider>();
                    ref var gridCellNum = ref manipulatorEntity.Get<InGridState>().GridCellNum;
                    ref var manipulatorGo = ref manipulatorEntity.Get<GameObjectProvider>().Value;

                    ref var blockEntity = ref manipulatorEntity.Get<IsHaveBlockState>().BlockEntity;
                    ref var blockGo = ref blockEntity.Get<GameObjectProvider>().Value;

                    foreach (var idg in _gridFilter)
                    {
                        ref var gridEntity = ref _gridFilter.GetEntity(idg);
                        ref var grid = ref gridEntity.Get<GridCell>();

                        if (grid.Id == gridCellNum)
                        {
                            if (grid.IsBusy)
                            {
                            }
                            else
                            {
                                blockGo.transform.SetParent(grid.Point);
                                //PrimeTween.Tween.LocalPosition(blockGo.transform, Vector3.zero, 0.5f);
                                blockEntity.Get<MyGrid>().Value = gridEntity;
                                grid.IsBusy = true;
                                grid.BlockEntity = blockEntity;
                                manipulatorEntity.Del<IsHaveBlockState>();
                                requestEntity.Del<TapRaycastEvent>();
                                _audioService.Play(Sounds.Place);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}