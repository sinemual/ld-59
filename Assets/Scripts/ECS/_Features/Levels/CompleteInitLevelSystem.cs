using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    
    public class CompleteInitLevelSystem : IEcsRunSystem
    {
        private PrefabFactory _factory;
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        private AudioService _audioService;
        private CameraService _cameraService;

        private EcsFilter<LevelProvider, CurrentLevelTag>.Exclude<InitedMarker> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var levelGo = ref entity.Get<GameObjectProvider>().Value;
                ref var levelProvider = ref entity.Get<LevelProvider>();
                _world.NewEntity().Get<StartLevelRequest>();
                entity.Get<InitedMarker>();
            }
        }
    }
}