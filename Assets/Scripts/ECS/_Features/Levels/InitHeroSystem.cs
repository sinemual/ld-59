using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class InitHeroSystem : IEcsRunSystem
    {
        private PrefabFactory _factory;
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        private AudioService _audioService;
        private CameraService _cameraService;

        private EcsFilter<HeroProvider>.Exclude<InitedMarker> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                entity.Get<InitedMarker>();
            }
        }
    }
}