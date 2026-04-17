using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class VelocityMovingSystem : IEcsRunSystem
    {
        private EcsFilter<VelocityMoving> _movingFilter;

        public void Run()
        {
            if (_movingFilter.IsEmpty())
                return;

            foreach (var movingObject in _movingFilter)
            {
                ref var movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref GameObjectProvider movingEntityGo = ref movingEntity.Get<GameObjectProvider>();
                ref RigidbodyProvider movingEntityRb = ref movingEntity.Get<RigidbodyProvider>();

                ref var moving = ref movingEntity.Get<VelocityMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                movingEntityRb.Value.linearVelocity +=
                    (moving.Target.position + moving.Offset - movingEntityGo.Value.transform.position).normalized * (moving.Speed * Time.deltaTime);

                if (Vector3.Distance(movingEntityGo.Value.transform.position, moving.Target.position + moving.Offset) <
                    moving.Accuracy)
                {
                    movingEntityRb.Value.linearVelocity = Vector3.zero;
                    movingEntity.Del<VelocityMoving>();
                    movingEntity.Get<MovingCompleteEvent>();
                }
            }
        }
    }
}