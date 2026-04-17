using System;
using Client.ECS.CurrentGame.Mining;
using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Client
{
    public class TransformMovingSystem : IEcsRunSystem
    {
        private EcsFilter<TransformMoving> _movingFilter;

        public void Run()
        {
            foreach (var movingObject in _movingFilter)
            {
                ref var movingEntity = ref _movingFilter.GetEntity(movingObject);
                ref var movingEntityGo = ref movingEntity.Get<GameObjectProvider>().Value;
                ref var moving = ref movingEntity.Get<TransformMoving>();

                moving.Speed = moving.Speed == 0 ? 2 : moving.Speed;
                moving.Accuracy = moving.Accuracy == 0 ? 0.1f : moving.Accuracy;

                /*Type[] types = new Type[movingEntity.GetComponentsCount()];
                movingEntity.GetComponentTypes(ref types);
                string who = "";
                foreach (var type in types)
                    who += $"{type}:";
                Debug.Log($"entity - {who}");*/
                movingEntityGo.transform.position = Vector3.MoveTowards(movingEntityGo.transform.position,
                    moving.Target.position, moving.Speed);

                if (Vector3.Distance(movingEntityGo.transform.position, moving.Target.position) < moving.Accuracy)
                {
                    movingEntity.Del<TransformMoving>();
                    movingEntity.Get<MovingCompleteEvent>();
                }
            }
        }
    }
}