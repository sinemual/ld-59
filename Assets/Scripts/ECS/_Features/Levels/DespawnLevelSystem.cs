using Client.Data;
using Client.Data.Core;
using Client.DevTools.MyTools;
using Client.ECS.CurrentGame.Level;
using Client.ECS.CurrentGame.Mining;
using Client.Factories;
using Client.Infrastructure.Services;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class DespawnLevelSystem : IEcsRunSystem
    {
        private SharedData _data;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;
        private DebugService _debugService;
        private EcsWorld _world;
        
        private EcsFilter<DespawnLevelRequest> _despawnRequestFilter;
        
        private EcsFilter<LevelProvider, CurrentLevelTag> _currentWorldFilter;
        private EcsFilter<FromThisLevelMarker> _worldEntitiesFilter;
        private EcsFilter<RestartLevelRequest> _restartReqFilter;
        private EcsFilter<SpawnLevelRequest> _spawnReqFilter;

        public void Run()
        {
            foreach (var request in _despawnRequestFilter)
            {
                DespawnCurrentWorld();
                _despawnRequestFilter.GetEntity(request).Del<DespawnLevelRequest>();
            }
        }

        private void DespawnCurrentWorld()
        {
            _debugService.LogSystemWork(this);

            foreach (var resReq in _restartReqFilter)
                _restartReqFilter.GetEntity(resReq).Get<Timer<DelayTimer>>().Value = 0.01f;
            
            foreach (var spwnReq in _spawnReqFilter)
                _spawnReqFilter.GetEntity(spwnReq).Get<Timer<DelayTimer>>().Value = 0.01f;
            
            foreach (var currentWorld in _currentWorldFilter)
                _prefabFactory.Despawn(ref _currentWorldFilter.GetEntity(currentWorld));

            foreach (var worldEntity in _worldEntitiesFilter)
            {
                
                _prefabFactory.Despawn(ref _worldEntitiesFilter.GetEntity(worldEntity));
            }
        }
    }
}