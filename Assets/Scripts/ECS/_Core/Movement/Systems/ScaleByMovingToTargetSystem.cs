using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class ScaleByMovingToTargetSystem : IEcsRunSystem
    {
        private EcsFilter<ScaleByMovingToTarget> _movingFilter;

        public void Run()
        {
            foreach (var movingObject in _movingFilter)
            {
                ref var movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref var movingEntityGo = ref movingEntity.Get<GameObjectProvider>().Value;
                ref var scaling = ref movingEntity.Get<ScaleByMovingToTarget>();

                movingEntityGo.transform.localScale *=
                    Vector3.Distance(movingEntityGo.transform.position, scaling.Target.position) / scaling.Distance;
            }
        }
    }
}