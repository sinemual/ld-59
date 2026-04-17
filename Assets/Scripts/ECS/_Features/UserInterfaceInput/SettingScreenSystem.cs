using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class SettingScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;

        public void Init()
        {
            _ui.GetScreen<SettingsScreen>().VibrationTriggerButtonTap += () =>
            {
                _data.SaveData.IsVibrationOn = !_data.SaveData.IsVibrationOn;
                _ui.GetScreen<SettingsScreen>().UpdateView();
            };

            _ui.GetScreen<SettingsScreen>().EffectsTriggerButtonTap += () =>
            {
                _data.SaveData.IsEffectsOn = !_data.SaveData.IsEffectsOn;
                _audioService.ToggleEffects(_data.SaveData.IsEffectsOn);
                _ui.GetScreen<SettingsScreen>().UpdateView();
            };

            _ui.GetScreen<SettingsScreen>().MusicTriggerButtonTap += () =>
            {
                _data.SaveData.IsMusicOn = !_data.SaveData.IsMusicOn;
                _audioService.ToggleMusic(_data.SaveData.IsMusicOn);
                _ui.GetScreen<SettingsScreen>().UpdateView();
            };

            _ui.GetScreen<SettingsScreen>().MusicVolumeChanged += (value) =>
            {
                _data.SaveData.MusicVolume = value;
                _audioService.ChangeMusicVolume(_data.SaveData.MusicVolume);
                _ui.GetScreen<SettingsScreen>().UpdateView();
            };

            _ui.GetScreen<SettingsScreen>().EffectsVolumeChanged += (value) =>
            {
                _data.SaveData.EffectsVolume = value;
                _audioService.ChangeEffectsVolume(_data.SaveData.EffectsVolume);
                _ui.GetScreen<SettingsScreen>().UpdateView();
            };

            _ui.GetScreen<SettingsScreen>().CloseButtonClick += () =>
            {
                Time.timeScale = 1.0f;
                _ui.HideScreen<SettingsScreen>();
                _audioService.Play(Sounds.UiClickSound);
                PokiUnitySDK.Instance.gameplayStart();
            };
        }
    }
}