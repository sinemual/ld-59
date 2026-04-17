using System;
using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.UI;
using Data;
using Leopotam.Ecs;
using UnityEngine;
using CameraType = Client.Data.CameraType;

namespace Client
{
    public class InitGameSystem : IEcsInitSystem
    {
        private SharedData _data;
        private UserInterface _ui;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;
        private TimeManagerService _timeManagerService;
        private CameraService _cameraService;
        
        public void Init()
        {
            /*if (_data.PlayerData.IsGameLaunchedBefore)
                _ui.MainMenuScreen.SetShowState(true);
            else
                _world.NewEntity().Get<SpawnLevelRequest>();*/
            _world.NewEntity().Get<SpawnLevelRequest>();

            //_world.NewEntity().Get<SpawnMenuLevelRequest>();

            _audioService.Play(Sounds.MusicMenuSound);
            _audioService.EffectSetLowpassCutoff(0);
            _audioService.MusicSetLowpassCutoff(0);
            
            //_cameraService.SetCamera(CameraType.None);

            _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.Init;
        }
    }
}