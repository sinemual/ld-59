using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Client
{
    public class DisposeLevelSystem : IEcsRunSystem
    {
        private PrefabFactory _prefabFactory;
        
        private EcsFilter<SpawnLevelRequest> _spawnRequestFilter;
        private EcsFilter<RestartLevelRequest> _restartRequestFilter;
        
        private EcsFilter<LevelProvider, CurrentLevelTag> _currentLevelFilter;

        public void Run()
        {
            foreach (var request in _spawnRequestFilter)
                DespawnCurrentLevel();
            
            foreach (var request in _restartRequestFilter)
                DespawnCurrentLevel();
        }

        private void DespawnCurrentLevel()
        {
            foreach (var currentLevel in _currentLevelFilter)
                _prefabFactory.Despawn(ref _currentLevelFilter.GetEntity(currentLevel));
        }
    }
}