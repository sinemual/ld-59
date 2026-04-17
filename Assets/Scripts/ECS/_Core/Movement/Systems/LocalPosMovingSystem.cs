using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LocalPosMovingSystem : IEcsRunSystem
    {
        private EcsFilter<LocalPositionMoving> _movingFilter;

        public void Run()
        {
            foreach (var movingObject in _movingFilter)
            {
                ref var movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref var movingEntityGo = ref movingEntity.Get<GameObjectProvider>().Value;
                ref var moving = ref movingEntity.Get<LocalPositionMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                movingEntityGo.transform.localPosition = Vector3.MoveTowards(movingEntityGo.transform.localPosition,
                    moving.LocalPosition, moving.Speed);

                if (Vector3.Distance(movingEntityGo.transform.localPosition, moving.LocalPosition) < moving.Accuracy)
                {
                    movingEntity.Del<LocalPositionMoving>();
                    movingEntity.Get<MovingCompleteEvent>();
                }
            }
        }
    }
}