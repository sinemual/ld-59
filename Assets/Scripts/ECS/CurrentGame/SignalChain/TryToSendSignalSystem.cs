using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class TryToSendSignalSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<SignalBlockProvider, MyGrid, SignalValue, OutputBlockMarker> _blockFilter;
        private EcsFilter<SignalStartButtonTapEvent> _eventFilter;
        private EcsFilter<SignalScreenProvider> _screenFilter;
        private EcsFilter<SignalScreenProvider, TimerDoneEvent<ErrorTimer>> _errorFilter;

        private EcsFilter<ProjectSolutionTag> _projectSolutionFilter;
        private EcsFilter<SignalStartButtonProvider> _buttonFilter;

        public void Run()
        {
            foreach (var idz in _errorFilter)
            {
                ref var screenEntity = ref _errorFilter.GetEntity(idz);
                ref var screen = ref screenEntity.Get<SignalScreenProvider>();
                screen.ErrorText.gameObject.SetActive(false);
            }

            if (_eventFilter.IsEmpty())
                return;

            foreach (var idx in _blockFilter)
            {
                ref var blockEntity = ref _blockFilter.GetEntity(idx);
                ref var signalValue = ref blockEntity.Get<SignalValue>().Value;

                foreach (var idz in _screenFilter)
                {
                    ref var screenEntity = ref _screenFilter.GetEntity(idz);
                    ref var screen = ref screenEntity.Get<SignalScreenProvider>();

                    _audioService.Play(Sounds.Signal);
                    screen.SignalText.text = $"SIGNAL: {signalValue}";

                    if (signalValue > _data.SaveData.Signal)
                    {
                        _data.SaveData.BestSignal = signalValue;
                        _world.NewEntity().Get<AddCurrencyRequest>().Value *= signalValue;
                    }
                    else
                    {
                        _world.NewEntity().Get<AddCurrencyRequest>().Value = signalValue;
                    }

                    screen.BestSignalText.text = $"BEST: {_data.SaveData.BestSignal}";
                    _data.SaveData.Signal = signalValue;

                    if (signalValue > _data.BalanceData.WinSignal)
                    {
                        _world.NewEntity().Get<SignalEndEvent>();
                    }
                    
                    _world.NewEntity().Get<SignalEvent>().Value = signalValue;
                }
            }

            if (_blockFilter.IsEmpty())
            {
                foreach (var idz in _screenFilter)
                {
                    ref var screenEntity = ref _screenFilter.GetEntity(idz);
                    ref var screen = ref screenEntity.Get<SignalScreenProvider>();

                    _audioService.Play(Sounds.Error);
                    screen.ErrorText.gameObject.SetActive(true);
                    screenEntity.Get<Timer<ErrorTimer>>().Value = 0.5f;
                }
            }
        }
    }

    public struct SignalEvent
    {
        public int Value;
    }

    public struct ErrorTimer
    {
        public float Value;
    }
}