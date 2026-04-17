using Client.DevTools.MyTools;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class DirectionalLaunchSystem : IEcsRunSystem
    {
        private EcsFilter<RigidbodyProvider, DirectionalLaunchRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var launchRequest = ref entity.Get<DirectionalLaunchRequest>();
                ref var entityRb = ref entity.Get<RigidbodyProvider>();

                Utility.ResetRigibodyVelocity(entityRb.Value);

                var direction = (launchRequest.Direction + Vector3.up * 2.0f) * (Random.Range(launchRequest.Force.x, launchRequest.Force.y));
                entity.Get<AddForce>() = new AddForce
                {
                    Direction = direction,
                    ForceMode = ForceMode.Impulse
                };

                entity.Get<LandingCheckRequest>();
                entity.Del<DirectionalLaunchRequest>();
            }
        }
    }
}