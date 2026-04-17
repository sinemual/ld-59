using Leopotam.Ecs;

namespace Client
{
    public class InitPathMovementSystem : IEcsRunSystem
    {
        private EcsFilter<PathMovementProvider> _movementFilter;
        private EcsFilter<PathProvider>.Exclude<InUseMarker> _pathFilter;

        public void Run()
        {
            foreach (var idx in _movementFilter)
            {
                ref EcsEntity pathUserEntity = ref _movementFilter.GetEntity(idx);
                ref PathMovementProvider pathMovementProvider = ref pathUserEntity.Get<PathMovementProvider>();
                ref SpawnData spawnData = ref pathUserEntity.Get<SpawnData>();
                
                foreach (var path in _pathFilter)
                {
                    ref EcsEntity pathEntity = ref _pathFilter.GetEntity(path);
                    ref PathProvider pathProvider = ref pathEntity.Get<PathProvider>();
                    
                    if (pathProvider.Id == spawnData.Id)
                    {
                        pathUserEntity.Get<StartMovingRequest>();
                        pathUserEntity.Get<SetCharacterStateRequest<IdleState>>();
                    
                        /*pathUserEntity.Get<HasPath>() = new HasPath()
                        {
                            CurrentPathPointIndex = 0,
                            Path = pathProvider,
                            CompleteRadius = 0.5f
                        };*/

                        pathEntity.Get<InUseMarker>();
                        break;
                    }
                }
            }
        }
    }
}