using System;
using Client.Data;
using Client.Data.Core;
using Client.ECS._Mechanics.Armor.Systems;
using Client.ECS.CurrentGame.Mining;
using Client.Factories;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using CameraType = Client.Data.CameraType;

namespace Client
{
    public class StartLevelSystem : IEcsRunSystem
    {
        private PrefabFactory _factory;
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        private PrefabFactory _prefabFactory;
        private CameraService _cameraService;

        private EcsFilter<StartLevelRequest>.Exclude<Timer<DelayTimer>> _filter;
        private EcsFilter<LevelProvider, InitedMarker, CurrentLevelTag> _levelFilter;
        private EcsFilter<HeroProvider> _heroFilter;

        public void Run()
        {
            foreach (var idx in _filter)
            {
                foreach (var idz in _levelFilter)
                {
                    ref var entity = ref _levelFilter.GetEntity(idz);
                    ref var levelProvider = ref entity.Get<LevelProvider>();

                    ref var reqEntity = ref _filter.GetEntity(idx);
                    _world.NewEntity().Get<StartWorldSoundRequest>();
                    //_world.NewEntity().Get<StartEarnEnergyRequest>();
                    _ui.ShowScreen<GameScreen>();
                    _ui.ShowScreen<SignalBlockScreen>();
                    _ui.ReorderScreens();
                    _world.NewEntity().Get<CheckInputToGameplayStartRequest>();
                    /*EcsEntity heroEntity = _prefabFactory.Spawn(_data.StaticData.CharacterDataByType[CharacterType.Hero].Prefab,
                        levelProvider.HeroSpawnPoint.position, levelProvider.HeroSpawnPoint.rotation);
                    var heroGo = heroEntity.Get<GameObjectProvider>().Value;

                    _cameraService.SetCamera(CameraType.PizzaReady, heroGo.transform, heroGo.transform);
                    _cameraService.SetDefaultTarget(heroGo.transform);*/
                    
                    

                    reqEntity.Del<StartLevelRequest>();

                    _world.NewEntity().Get<EarnCurrencyEvent>();

                    _ui.GetScreen<GameScreen>().UpdateLevelText(_data.SaveData.EventLevelIdx);
                }
            }
        }
    }
}