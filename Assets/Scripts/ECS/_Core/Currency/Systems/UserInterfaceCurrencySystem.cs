using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class UserInterfaceCurrencySystem : IEcsInitSystem, IEcsRunSystem
    {
        private SharedData _data;
        private UserInterface _ui;

        private EcsFilter<EarnCurrencyEvent> _earnEventFilter;
        private EcsFilter<SpendCurrncyEvent> _spendEventFilter;

        public void Init() => UpdateTexts();

        public void Run()
        {
            foreach (var idx in _earnEventFilter)
                UpdateTexts();

            foreach (var idx in _spendEventFilter)
                UpdateTexts();
        }

        private void UpdateTexts()
        {
            _ui.GetScreen<GameScreen>().UpdateMoneyText(_data.SaveData.Currency);
        }
    }
}