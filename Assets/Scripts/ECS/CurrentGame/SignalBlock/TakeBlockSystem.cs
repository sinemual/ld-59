using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TakeBlockSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<TapRaycastEvent> _tapRequestFilter;
        private EcsFilter<ManipulatorProvider, InGridState>.Exclude<IsHaveBlockState> _manipulatorFilter;
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

                    foreach (var idg in _gridFilter)
                    {
                        ref var gridEntity = ref _gridFilter.GetEntity(idg);
                        ref var grid = ref gridEntity.Get<GridCell>();

                        if (grid.Id == gridCellNum)
                        {
                            if (grid.IsBusy)
                            {
                                ref var blockEntity = ref grid.BlockEntity;
                                ref var blockGo = ref blockEntity.Get<GameObjectProvider>().Value;
                                blockGo.transform.SetParent(manipulatorGo.transform);
                                blockGo.transform.localPosition = Vector3.zero;
                                //PrimeTween.Tween.LocalPosition(blockGo.transform, Vector3.up, 0.5f);
                                manipulatorEntity.Get<IsHaveBlockState>().BlockEntity = blockEntity;
                                grid.IsBusy = false;
                                requestEntity.Del<TapRaycastEvent>();
                            }
                        }
                    }
                }
            }
        }
    }
}