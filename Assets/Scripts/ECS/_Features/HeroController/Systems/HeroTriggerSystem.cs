using Client.Data;
using Client.Data.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class HeroTriggerSystem : IEcsRunSystem
    {
        private SharedData _data;

        private EcsFilter<HeroProvider, OnTriggerEnterEvent>.Exclude<CurrentCharacterState<DeadState>> _enterFilter;
        private EcsFilter<HeroProvider, OnTriggerExitEvent>.Exclude<CurrentCharacterState<DeadState>> _exitFilter;

        public void Run()
        {
            foreach (var idx in _enterFilter)
            {
                ref var entity = ref _enterFilter.GetEntity(idx);
                ref var entityCollision = ref entity.Get<OnTriggerEnterEvent>();
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;

                if (entityCollision.Collider && entityCollision.Collider.TryGetComponent(out MonoEntity monoEntity))
                {
                    if (monoEntity.Entity.IsAlive() && !monoEntity.Entity.Has<PickupState>())
                    {
                        monoEntity.Entity.Get<PickupState>();
                        monoEntity.Entity.Get<RigidbodyProvider>().Value.isKinematic = true;
                        monoEntity.Entity.Get<TransformAroundMoving>() = new TransformAroundMoving()
                        {
                            Target = entityGo.transform,
                            Speed = 2.0f,
                            Accuracy = 1.0f,
                            Radius = 1.0f,
                            Offset = Vector3.up
                        };
                    }
                }
            }

            foreach (var idx in _exitFilter)
            {
                ref var entity = ref _exitFilter.GetEntity(idx);
                ref var entityCollision = ref entity.Get<OnTriggerExitEvent>();

                if (entityCollision.Collider.gameObject.CompareTag(_data.StaticData.GroundTag))
                {
                }
            }
        }
    }


    public struct PickupState : IEcsIgnoreInFilter
    {
    }
}