using Client.Data.Core;
using Leopotam.Ecs;

namespace Client
{
    public class PatrolNavPathMovementSystem : IEcsRunSystem
    {
        private SharedData _data;

        private EcsFilter<NavMeshAgentProvider, HasPath, MovingCompleteEvent> _completeFilter;
        private EcsFilter<NavMeshAgentProvider, HasPath, TimerDoneEvent<TimerForPathIdle>>.Exclude<DeadState> _idleFilter;

        public void Run()
        {
            foreach (var idx in _completeFilter)
            {
                ref var entity = ref _completeFilter.GetEntity(idx);
                ref HasPath hasPath = ref entity.Get<HasPath>();
                ref var navMeshAgent = ref entity.Get<NavMeshAgentProvider>();
                ref var movementProvider = ref entity.Get<PathMovementProvider>();

                hasPath.CurrentPathPointIndex++;
                if (movementProvider.IsIdleAtPathEnd &&
                    (hasPath.CurrentPathPointIndex == 0 || hasPath.CurrentPathPointIndex == 1))
                {
                    entity.Get<Timer<TimerForPathIdle>>().Value = 3.0f; //_gameData.BalanceData.PatrolIdleTime;
                    if (navMeshAgent.Value.hasPath)
                    {
                        navMeshAgent.Value.ResetPath();
                        navMeshAgent.Value.SetDestination(hasPath.Path.Value[hasPath.CurrentPathPointIndex].position);
                    }

                    navMeshAgent.Value.enabled = false;
                    entity.Del<MovingState>();
                    continue;
                }

                if (navMeshAgent.Value.enabled)
                    navMeshAgent.Value.SetDestination(hasPath.Path.Value[hasPath.CurrentPathPointIndex].position);
            }


            foreach (var idx in _idleFilter)
            {
                ref var entity = ref _idleFilter.GetEntity(idx);

                entity.Get<StartMovingRequest>();
            }
        }
    }
}