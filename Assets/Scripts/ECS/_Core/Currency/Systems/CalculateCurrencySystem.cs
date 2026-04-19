using Client.Data;
using Client.Data.Core;
using Leopotam.Ecs;

namespace Client
{
    public class CalculateCurrencySystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private AudioService _audioService;

        private EcsFilter<AddCurrencyRequest> _addFilter;
        private EcsFilter<SubtractCurrencyRequest> _subtractFilter;

        public void Run()
        {
            foreach (var idx in _addFilter)
            {
                ref var entity = ref _addFilter.GetEntity(idx);
                _data.SaveData.Currency += _addFilter.Get1(idx).Value;

                _world.NewEntity().Get<EarnCurrencyEvent>().Value = entity.Get<AddCurrencyRequest>().Value;
                entity.Del<AddCurrencyRequest>();
                _audioService.Play(Sounds.Money);
                
            }

            foreach (var idx in _subtractFilter)
            {
                ref var entity = ref _subtractFilter.GetEntity(idx);
                _data.SaveData.Currency -= _subtractFilter.Get1(idx).Value;
                if (_data.SaveData.Currency < 0)
                    _data.SaveData.Currency = 0;

                _world.NewEntity().Get<SpendCurrncyEvent>().Value = entity.Get<SubtractCurrencyRequest>().Value;
                entity.Del<SubtractCurrencyRequest>();
            }
        }
    }
}