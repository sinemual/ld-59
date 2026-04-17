using System.Collections.Generic;
using Client.Data.Core;
using Client.ECS.CurrentGame.Damage.Providers;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.ECS._Mechanics.Armor.Systems
{
    public class WorldUiSystem : IEcsRunSystem
    {
        private SharedData _data;
        private UserInterface _ui;
        private CameraService _cameraService;

        private EcsFilter<WorldUiProvider> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);
                ref var worldUi = ref entity.Get<WorldUiProvider>();
                ref var targetPoint = ref entity.Get<TargetPoint>().Value;
                
                var screenPosition = _cameraService.GetCamera().WorldToScreenPoint(targetPoint.position + Vector3.up * 3.0f);
                worldUi.Panel.transform.position = screenPosition;
            }
        }
    }
}