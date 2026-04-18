using System.Collections.Generic;
using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class GameScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;
        
        private EcsFilter<HeroProvider> _playerFilter;

        public void Init()
        {
            _ui.GetScreen<GameScreen>().PauseButtonClick += () =>
            {
                Time.timeScale = 0.0f;
                _world.NewEntity().Get<SetGameStateRequest>().NewGameStateType = GameStateType.Pause;
                _ui.ShowScreen<SettingsScreen>();
                _ui.GetScreen<SettingsScreen>().UpdateView();
                _audioService.Play(Sounds.UiClickSound);
            };
        }
    }
}