using Client.Data;
using Client.Data.Core;
using Client.Infrastructure.UI;
using Leopotam.Ecs;

namespace Client
{
    public class SignalBlockScreenSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private SharedData _data;
        private UserInterface _ui;
        private AudioService _audioService;

        public void Init()
        {
            _ui.GetScreen<SignalBlockScreen>().SignalBlockPanels.BuyBlock += (blockData) =>
            {
                _audioService.Play(Sounds.UiClickSound);

                _world.NewEntity().Get<BuyBlockRequest>() = new BuyBlockRequest()
                {
                    SignalBlockType = blockData.SignalBlockType,
                    InputDirection = blockData.InputDirection
                };
                _world.NewEntity().Get<SubtractCurrencyRequest>().Value = blockData.BuyPrice;
                _ui.GetScreen<SignalBlockScreen>().SignalBlockPanels.InitItems(_data.StaticData.SignalBlockDatas, _data.SaveData.Currency,
                    _data.RuntimeData.GetResearchPoints(), false);
            };

            _ui.GetScreen<SignalBlockScreen>().ShowScreen += () =>
            {
                //var isTutorialStep = !_data.SaveData.TutrorialStates[TutorialStep.BuyItem];
                _ui.GetScreen<SignalBlockScreen>().SignalBlockPanels.InitItems(_data.StaticData.SignalBlockDatas, _data.SaveData.Currency,
                    _data.RuntimeData.GetResearchPoints(), false);
            };

            _ui.GetScreen<SignalBlockScreen>().CloseButtonClick += () =>
            {
                _ui.HideScreen<SignalBlockScreen>();
                _audioService.Play(Sounds.UiClickSound);
            };
            
            _ui.GetScreen<SignalBlockScreen>().DeleteModeButtonClick += () =>
            {
                _data.RuntimeData.IsDeleteState = !_data.RuntimeData.IsDeleteState;
                _world.NewEntity().Get<ChangeDeleteModeStateRequest>();
            };
        }
    }

    public struct ChangeDeleteModeStateRequest : IEcsIgnoreInFilter
    {
    }
}