using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using PrimeTween;
using UnityEditor.Build.Pipeline.Utilities;
using UnityEngine;

namespace Client
{
    public class SignalReactSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<SignalEvent> _signalFilter;
        private EcsFilter<SignalStartButtonProvider> _buttonFilter;
        private EcsFilter<SignalScreenProvider> _screenFilter;

        private Sequence _sequence;
        
        public void Run()
        {
            foreach (var idx in _signalFilter)
            {
                ref var blockEntity = ref _signalFilter.GetEntity(idx);
                ref var signalValue = ref blockEntity.Get<SignalEvent>().Value;

                foreach (var idy in _buttonFilter)
                {
                    ref var buttonEntity = ref _buttonFilter.GetEntity(idy);
                    ref var button = ref buttonEntity.Get<SignalStartButtonProvider>();
                    button.TapVfx.Play();
                    button.RadarVfx.transform.localScale = Vector3.one * (signalValue * _data.BalanceData.FovCoef);
                    button.RadarVfx.Play();
                }
                
                foreach (var idz in _screenFilter)
                {
                    ref var screenEntity = ref _screenFilter.GetEntity(idz);
                    ref var screen = ref screenEntity.Get<SignalScreenProvider>();

                    CameraPlay(screen.SpaceCamera, signalValue);
                }
            }
        }
        
        private void CameraPlay(Camera camera, float targetFov)
        {
            _sequence.Stop();

            camera.fieldOfView = _data.BalanceData.StartFov;
            var target = Mathf.Max(_data.BalanceData.StartFov + 1.0f, _data.BalanceData.FovCoef * targetFov);
            _sequence = Sequence.Create()
                .Chain(Tween.CameraFieldOfView(camera, target, _data.BalanceData.ButtonPressDuration))
                .ChainDelay(0.3f)
                .Chain(Tween.CameraFieldOfView(camera, _data.BalanceData.StartFov, _data.BalanceData.ButtonPressDuration));
        }
    }
}