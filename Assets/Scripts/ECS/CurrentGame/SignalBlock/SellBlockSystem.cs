using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class SellBlockSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<TapRaycastEvent> _tapRequestFilter;
        private EcsFilter<ManipulatorProvider, IsHaveBlockState>.Exclude<InGridState> _manipulatorFilter;
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
                    ref var signalBlockData = ref blockEntity.Get<SignalBlockDataComponent>().Value;

                    _world.NewEntity().Get<AddCurrencyRequest>().Value =
                        (int)(signalBlockData.BuyPrice * 0.5f);
                    _prefabFactory.Despawn(ref blockEntity);
                    manipulatorEntity.Del<IsHaveBlockState>();
                    _audioService.Play(Sounds.Money);
                }
            }
        }
    }
}