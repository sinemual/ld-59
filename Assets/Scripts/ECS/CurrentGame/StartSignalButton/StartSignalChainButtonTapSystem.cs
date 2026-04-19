using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using PrimeTween;
using UnityEngine;

namespace Client
{
    public class StartSignalChainButtonTapSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;
        private AudioService _audioService;

        private EcsFilter<TapRaycastEvent> _tapRequestFilter;
        private EcsFilter<SignalStartButtonProvider> _buttonFilter;

        private Sequence _sequence;
        
        public void Run()
        {
            foreach (var idx in _tapRequestFilter)
            {
                ref var requestEntity = ref _tapRequestFilter.GetEntity(idx);
                ref var request = ref requestEntity.Get<TapRaycastEvent>();

                if (request.GameObject.TryGetComponent(out MonoEntity monoEntity))
                {
                    if (monoEntity.Entity.Has<SignalStartButtonProvider>())
                    {
                        _world.NewEntity().Get<SignalStartButtonTapEvent>();
                        _audioService.Play(Sounds.Button);

                        foreach (var idz in _buttonFilter)
                        {
                            ref var buttonEntity = ref _buttonFilter.GetEntity(idz);
                            ref var button = ref buttonEntity.Get<SignalStartButtonProvider>();
                            Play(button.ButtonTransform.transform, button.ButtonDisabled);
                        }
                    }
                }
            }
        }

        private void Play(Transform buttonRoot, GameObject visual)
        {
            _sequence.Stop();

            buttonRoot.localPosition = _data.BalanceData.ButtonLocalPos;
            buttonRoot.localScale = _data.BalanceData.ButtonStartScale;
            visual.SetActive(false);

            Vector3 pressedPos = _data.BalanceData.ButtonLocalPos + Vector3.down * 0.1f;
            Vector3 pressedScale = new Vector3(
                _data.BalanceData.ButtonStartScale.x * 1.06f,
                _data.BalanceData.ButtonStartScale.y * 0.9f,
                _data.BalanceData.ButtonStartScale.z * 1.06f);

            _sequence = Sequence.Create()

                .Group(Tween.LocalPosition(buttonRoot, pressedPos, _data.BalanceData.ButtonPressDuration))
                .Group(Tween.Scale(buttonRoot, pressedScale, _data.BalanceData.ButtonPressDuration))

                .ChainCallback(() => visual.SetActive(true))
                .ChainDelay(_data.BalanceData.ButtonPressDuration * 0.2f)

                .Group(Tween.LocalPosition(buttonRoot, _data.BalanceData.ButtonLocalPos, _data.BalanceData.ButtonPressDuration))
                .Group(Tween.Scale(buttonRoot, _data.BalanceData.ButtonStartScale, _data.BalanceData.ButtonPressDuration))
                .ChainCallback(() => visual.SetActive(false));
        }
    }
}