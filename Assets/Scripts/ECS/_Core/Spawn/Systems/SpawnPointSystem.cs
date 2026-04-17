using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class SpawnPointSystem : IEcsRunSystem
    {
        private EcsWorld _world;

        private PrefabFactory _prefabFactory;
        
        private EcsFilter<SpawnPointProvider, SpawnPointDataProvider>.Exclude<WorkedOutMarker> _filter;
        
        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var spawnPoint = ref entity.Get<SpawnPointProvider>().Value;
                ref var spawnPointGo = ref entity.Get<GameObjectProvider>();
                ref var spawnPointData = ref entity.Get<SpawnPointDataProvider>();

                if (spawnPointGo.Value.activeInHierarchy)
                {
                    EcsEntity spawnEntity = _prefabFactory.Spawn(spawnPointData.Prefab, spawnPoint.position, spawnPoint.rotation, spawnPointGo.Value.transform);

                    spawnEntity.Get<SpawnData>() = new SpawnData()
                    {
                        SpawnEntity = entity,
                        Point = spawnPointGo.Value.transform,
                        Id = spawnPointData.Id
                    };
                    
                    entity.Get<WorkedOutMarker>();
                }
            }
        }
    }
}