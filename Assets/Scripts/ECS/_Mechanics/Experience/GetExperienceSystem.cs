using Client.Data.Core;
using Client.Infrastructure;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.ECS.CurrentGame.Experience
{
    public class GetExperienceSystem : IEcsRunSystem
    {
        private SharedData _data;
        private UserInterface _ui;

        private EcsFilter<GetExperienceRequest> _filter;

        public void Run()
        {
            foreach (int index in _filter)
            {
                EcsEntity entity = _filter.GetEntity(index);
                int experience = entity.Get<GetExperienceRequest>().Value;
                
                entity.Del<GetExperienceRequest>();
            }
        }
    }
}