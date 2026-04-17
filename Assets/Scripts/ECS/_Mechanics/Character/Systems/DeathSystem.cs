using System;
using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Client.Infrastructure.Services;
using Client.Infrastructure.UI;
using Data;
using Leopotam.Ecs;
using Quaternion = UnityEngine.Quaternion;

namespace Client
{
    public class DeathSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private UserInterface _ui;
        private PrefabFactory _factory;
        private VfxService _vfxService;
        private DebugService _debugService;
        private AudioService _audioService;

        private EcsFilter<DeathEvent>.Exclude<Timer<DeadDespawnTimer>> _deathFilter;

        public void Run()
        {
            foreach (var idx in _deathFilter)
            {
                _debugService.LogSystemWork(this);

                ref var entity = ref _deathFilter.GetEntity(idx);
                ref var deadEntity = ref entity.Get<DeathEvent>().Dead;
                ref var deadGo = ref deadEntity.Get<GameObjectProvider>().Value;

                deadEntity.Get<DeadState>();
                if (deadEntity.Has<CharacterProvider>())
                {
                    ref var deadCharacter = ref deadEntity.Get<CharacterProvider>();
                    ref var animator = ref deadEntity.Get<AnimatorProvider>().Value;
                    deadCharacter.Collider.enabled = false;
                    deadCharacter.DeathCollider.enabled = true;

                    animator.SetTrigger(Animations.IsDeath);
                    _audioService.Play(Sounds.GetRandomZombieHit());
                }

                if (deadEntity.Has<StateCanvasProvider>())
                {
                    ref var canvas = ref deadEntity.Get<StateCanvasProvider>();
                    canvas.Canvas.SetActive(false);
                }

                //deadEntity.Get<DespawnAtTimerRequest>();
                //deadEntity.Get<Timer<DespawnTimer>>().Value = _data.BalanceData.TimeToDespawnDeadBody;
            }
        }
    }
}