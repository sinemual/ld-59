using Client.Data;
using Client.Data.Core;
using Client.DevTools.MyTools;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using CameraType = Client.Data.CameraType;

namespace Client
{
    public class LevelProgressSystem : IEcsRunSystem
    {
        private SharedData _data;
        private UserInterface _ui;
        private EcsWorld _world;
        private CameraService _cameraService;

        private EcsFilter<LevelProvider, InitedMarker> _levelFilter;
        private EcsFilter<HeroProvider> _carFilter;

        public void Run()
        {
            foreach (var lvl in _levelFilter)
            {
                ref var levelEntity = ref _levelFilter.GetEntity(lvl);
                //_world.NewEntity().Get<LevelCompleteRequest>();
            }
        }
    }
}