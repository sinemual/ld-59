using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.Services;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class RotationObjectsSystem : IEcsRunSystem
    {
        private EcsFilter<RotateObjectProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var entityGo = ref entity.Get<GameObjectProvider>().Value;
                ref var speed = ref entity.Get<RotateObjectProvider>().Speed;

                if (entityGo.activeInHierarchy)
                    entityGo.transform.Rotate(0f, speed * Time.deltaTime, 0f);
            }
        }
    }
}