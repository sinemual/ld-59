using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using PrimeTween;
using TMPro;
using UnityEngine;
using CameraType = Client.Data.CameraType;
using Debug = UnityEngine.Debug;

namespace Client
{
    public class GameEndSystem : IEcsRunSystem
    {
        private UserInterface _ui;
        private EcsWorld _world;
        private SharedData _data;
        private AudioService _audioService;
        private CameraService _cameraService;

        private EcsFilter<ProjectSolutionEvent> _projectSolutionFilter;
        private EcsFilter<SignalEndEvent> _signalFilter;

        private EcsFilter<SignalScreenProvider> _screenFilter;

        private Tween _textTween;

        public void Run()
        {
            if(_data.RuntimeData.IsEnded)
                return;
            
            foreach (var idx in _projectSolutionFilter)
            {
                ref var entity = ref _projectSolutionFilter.GetEntity(idx);
                foreach (var idz in _screenFilter)
                {
                    ref var screenEntity = ref _screenFilter.GetEntity(idz);
                    ref var screen = ref screenEntity.Get<SignalScreenProvider>();
                    screen.EndText.gameObject.SetActive(true);
                    screen.SignalText.gameObject.SetActive(false);
                    Play(_data.StaticData.ProjectSolutionEndText, screen.EndText, 5.0f);
                    screen.ProjectSolutionCharacter.gameObject.SetActive(true);
                    _data.RuntimeData.IsEnded = true;
                    _cameraService.SetCamera(CameraType.ToScreen);
                }
            }
            
            foreach (var idx in _signalFilter)
            {
                ref var entity = ref _signalFilter.GetEntity(idx);
                foreach (var idz in _screenFilter)
                {
                    ref var screenEntity = ref _screenFilter.GetEntity(idz);
                    ref var screen = ref screenEntity.Get<SignalScreenProvider>();
                    screen.EndText.gameObject.SetActive(true);
                    screen.SignalText.gameObject.SetActive(false);
                    Play(_data.StaticData.SignalEndText, screen.EndText, 5.0f);
                    screen.SignalCharacter.gameObject.SetActive(true);
                    _data.RuntimeData.IsEnded = true;
                    _cameraService.SetCamera(CameraType.ToScreen);
                }
            }
        }

        private void Play(string fullText, TextMeshProUGUI tmp, float duration)
        {
            _textTween.Stop();

            tmp.text = fullText;
            tmp.ForceMeshUpdate();
            tmp.maxVisibleCharacters = 0;

            int totalChars = tmp.textInfo.characterCount;

            _textTween = Tween.Custom(
                0,
                totalChars,
                duration,
                onValueChange: value => { tmp.maxVisibleCharacters = Mathf.FloorToInt(value); });
        }
    }
}