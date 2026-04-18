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

                    screen.SignalText.text = $"SIGNAL: {signalValue}";
                }
            }
        }
    }
}