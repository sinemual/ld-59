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

        public void Run()
        {
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
                }
            }

            if (_blockFilter.IsEmpty()) //chain is not build
            {
                _audioService.Play(Sounds.Error);
            }
        }
    }
}