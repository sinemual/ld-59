using Client.Data;
using Client.Data.Core;
using Client.ECS.CurrentGame.Mining;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class AudioSystem : IEcsInitSystem, IEcsRunSystem
    {
        private SharedData _data;
        private AudioService _audioService;
        private UserInterface _userInterface;
        private EcsWorld _world;

        private EcsFilter<InitVolumeRequest> _reqFilter;

        public void Init()
        {
            EcsEntity req = _world.NewEntity();
            req.Get<Timer<DelayTimer>>().Value = 0.5f;
            req.Get<InitVolumeRequest>();
        }

        public void Run()
        {
            foreach (var idx in _reqFilter)
            {
                ref var reqEntity = ref _reqFilter.GetEntity(idx);
                reqEntity.Del<InitVolumeRequest>();
                
                _audioService.ChangeMusicVolume(_data.SaveData.MusicVolume);
                _audioService.ChangeEffectsVolume(_data.SaveData.EffectsVolume);
            }
        }
    }

    internal struct InitVolumeRequest : IEcsIgnoreInFilter
    {
    }
}