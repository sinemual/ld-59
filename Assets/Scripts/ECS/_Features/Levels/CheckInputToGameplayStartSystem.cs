using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class CheckInputToGameplayStartSystem : IEcsRunSystem
    {
        private PrefabFactory _factory;
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        private PrefabFactory _prefabFactory;
        private CameraService _cameraService;

        private EcsFilter<CheckInputToGameplayStartRequest> _filter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                ref var entity = ref _filter.GetEntity(idx);

                if (Input.GetMouseButton(0) || Input.GetAxisRaw("Vertical") != 0)
                {
                    entity.Del<CheckInputToGameplayStartRequest>();
                    PokiUnitySDK.Instance.gameplayStart();
                }
            }
        }
    }
}