using Client.Data.Core;
using Client.ECS.CurrentGame.Level;
using Client.ECS.CurrentGame.Mining;
using Client.Factories;
using Client.Infrastructure.Services;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class SpawnLevelSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private SharedData _data;

        private PrefabFactory _prefabFactory;
        private CleanService _cleanService;
        private DebugService _debugService;

        private EcsFilter<SpawnLevelRequest>.Exclude<Timer<DelayTimer>> _initFilter;
        private EcsFilter<RestartLevelRequest>.Exclude<Timer<DelayTimer>> _restartFilter;

        public void Run()
        {
            foreach (var idx in _initFilter)
            {
                InitWorld();
                _initFilter.GetEntity(idx).Del<SpawnLevelRequest>();
            }

            foreach (var idx in _restartFilter)
            {
                RestartLevel();
                _restartFilter.GetEntity(idx).Del<RestartLevelRequest>();
            }
        }

        private void InitWorld()
        {
            if (_data.SaveData.LevelIdx >= _data.StaticData.Levels.Count - 1)
            {
                _data.SaveData.LevelIdx = 2;
                _data.RuntimeData.IsLoopedLevel = true;
                SpawnWorldLooped();
                return;
            }

            _data.RuntimeData.LastLevelIdx = _data.SaveData.LevelIdx;

            SpawnWorld(_data.SaveData.LevelIdx);
            _debugService.Log($"Create level with idx: {_data.SaveData.LevelIdx}, max lvl: {_data.StaticData.Levels.Count - 1}");
        }

        private void RestartLevel()
        {
            _data.SaveData.LevelIdx = _data.RuntimeData.LastLevelIdx;
            InitWorld();
        }

        private void SpawnWorld(int levelNum)
        {
            EcsEntity entity = _prefabFactory.Spawn(_data.StaticData.Levels[levelNum].Prefab, Vector3.zero, Quaternion.identity);
            entity.Get<CurrentLevelTag>();
            _prefabFactory.SetDefaultParent(entity.Get<GameObjectProvider>().Value.transform);
        }

        private void SpawnWorldLooped()
        {
            EcsEntity entity = _prefabFactory.Spawn(_data.StaticData.LevelLooped.Prefab, Vector3.zero, Quaternion.identity);
            entity.Get<CurrentLevelTag>();
            _prefabFactory.SetDefaultParent(entity.Get<GameObjectProvider>().Value.transform);
        }
    }
}