using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class DeleteModeSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<TapRaycastEvent> _tapRequestFilter;
        private EcsFilter<ManipulatorProvider>.Exclude<IsHaveBlockState> _manipulatorFilter;
        private EcsFilter<GridCell> _gridFilter;

        public void Run()
        {
            if (_data.RuntimeData.IsDeleteState)
            {
                foreach (var idx in _tapRequestFilter)
                {
                    ref var requestEntity = ref _tapRequestFilter.GetEntity(idx);
                    ref var request = ref requestEntity.Get<TapRaycastEvent>();

                    foreach (var idm in _manipulatorFilter)
                    {
                        ref var manipulatorEntity = ref _manipulatorFilter.GetEntity(idm);

                        if (manipulatorEntity.Has<InGridState>())
                        {
                            if (request.GameObject.TryGetComponent(out MonoEntity monoEntity))
                            {
                                if (monoEntity.Entity.Has<MyGrid>())
                                {
                                    monoEntity.Entity.Get<MyGrid>().Value.Get<GridCell>().IsBusy = false;
                                    ref var signalBlockData = ref monoEntity.Entity.Get<SignalBlockDataComponent>().Value;
                                    _world.NewEntity().Get<AddCurrencyRequest>().Value = (int)(signalBlockData.BuyPrice * 0.5f);
                                    _prefabFactory.Despawn(ref monoEntity.Entity);
                                    requestEntity.Del<TapRaycastEvent>();
                                }
                            }
                        }
                        else
                        {
                            _data.RuntimeData.IsDeleteState = false;
                        }
                    }
                }
            }
        }
    }
}