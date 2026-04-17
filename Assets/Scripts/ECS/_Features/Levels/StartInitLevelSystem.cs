using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class StartInitLevelSystem : IEcsRunSystem
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

                _data.RuntimeData.ResetLevelData();
            }
        }
    }
}
