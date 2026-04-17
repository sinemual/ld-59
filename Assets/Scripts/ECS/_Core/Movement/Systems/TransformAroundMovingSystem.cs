using Client.ECS.CurrentGame.Mining;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TransformAroundMovingSystem : IEcsRunSystem
    {
        private EcsFilter<TransformAroundMoving> _movingFilter;

        public void Run()
        {
            foreach (var movingObject in _movingFilter)
            {
                ref var movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref var movingEntityGo = ref movingEntity.Get<GameObjectProvider>().Value;
                ref var moving = ref movingEntity.Get<TransformAroundMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                Vector3 aroundPosition;
                if (moving.Target)
                    aroundPosition = moving.Target.position + moving.Offset * moving.Radius;
                else
                    aroundPosition = moving.TargetPosition + moving.Offset * moving.Radius;

                movingEntityGo.transform.position = Vector3.MoveTowards(movingEntityGo.transform.position,
                    aroundPosition, moving.Speed);

                if (Vector3.Distance(movingEntityGo.transform.position, aroundPosition) < moving.Accuracy)
                {
                    movingEntity.Get<Timer<DelayTimer>>().Value = 0.2f;
                    movingEntity.Get<AroundMovingCompleteEvent>();
                    movingEntity.Get<TransformMoving>() = new TransformMoving()
                    {
                        Target = moving.Target,
                        Accuracy = moving.Accuracy,
                        Speed = moving.Speed
                    };
                    movingEntity.Del<TransformAroundMoving>();
                }
            }
        }
    }
}