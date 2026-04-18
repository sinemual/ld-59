using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class StartSignalChainButtonTapSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<TapRaycastEvent> _tapRequestFilter;

        public void Run()
        {
            foreach (var idx in _tapRequestFilter)
            {
                ref var requestEntity = ref _tapRequestFilter.GetEntity(idx);
                ref var request = ref requestEntity.Get<TapRaycastEvent>();

                if (request.GameObject.TryGetComponent(out MonoEntity monoEntity))
                {
                    if (monoEntity.Entity.Has<SignalStartButtonProvider>())
                    {
                        _world.NewEntity().Get<SignalStartButtonTapEvent>();
                    }
                }
            }
        }
    }
}