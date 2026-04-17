using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Client
{
    public class GameOverSystem : IEcsRunSystem
    {
        private UserInterface _ui;
        private EcsWorld _world;
        private SharedData _data;
        private AudioService _audioService;

        private EcsFilter<DeathEvent> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var deadEntity = ref entity.Get<DeathEvent>().Dead;
                if (deadEntity.Has<HeroProvider>())
                {
                    _data.RuntimeData.CurrentGameStateType = GameStateType.GameOver;
                    _world.NewEntity().Get<GameFailedEvent>();
                }
            }
        }
    }
}