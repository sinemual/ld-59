using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Mining;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class StartLevelSoundSystem : IEcsRunSystem
    {
        private PrefabFactory _factory;
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        private AudioService _audioService;
        
        private EcsFilter<StartWorldSoundRequest>.Exclude<Timer<DelayTimer>> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var worldGo = ref entity.Get<GameObjectProvider>().Value;

                entity.Del<StartWorldSoundRequest>();
            }
        }
    }
}