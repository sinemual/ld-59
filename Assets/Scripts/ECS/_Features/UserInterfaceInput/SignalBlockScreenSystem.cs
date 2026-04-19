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
            _data.RuntimeData.TabType = SignalBlockType.OneSignal;
            _data.SaveData.IsBlockResearched[SignalBlockType.OneSignal] = true;

            _ui.GetScreen<SignalBlockScreen>().SignalBlockPanels.BuyBlock += (blockData) =>
            {
                _audioService.Play(Sounds.UiClickSound);

                _world.NewEntity().Get<BuyBlockRequest>() = new BuyBlockRequest()
                {
                    SignalBlockType = blockData.SignalBlockType,
                    InputDirection = blockData.InputDirection
                };
                _data.SaveData.IsBlockResearched[blockData.SignalBlockType] = true;
                _world.NewEntity().Get<SubtractCurrencyRequest>().Value = blockData.BuyPrice;
                _ui.GetScreen<SignalBlockScreen>().SignalBlockPanels.InitItems(_data.StaticData.BlocksByType[_data.RuntimeData.TabType].Blocks,
                    _data.SaveData.Currency,
                    _data.SaveData.IsBlockResearched[_data.RuntimeData.TabType], false);
                _ui.GetScreen<SignalBlockScreen>()
                    .UpdateDescriptionText(_data.StaticData.SignalBlockDataByType[_data.RuntimeData.TabType].BlockDescription);
            };

            _ui.GetScreen<SignalBlockScreen>().ShowScreen += () =>
            {
                //var isTutorialStep = !_data.SaveData.TutrorialStates[TutorialStep.BuyItem];
                _ui.GetScreen<SignalBlockScreen>().SignalBlockPanels.InitItems(_data.StaticData.BlocksByType[_data.RuntimeData.TabType].Blocks,
                    _data.SaveData.Currency,
                    _data.SaveData.IsBlockResearched[_data.RuntimeData.TabType], false);
                _ui.GetScreen<SignalBlockScreen>()
                    .UpdateDescriptionText(_data.StaticData.SignalBlockDataByType[_data.RuntimeData.TabType].BlockDescription);
            };

            _ui.GetScreen<SignalBlockScreen>().CloseButtonClick += () =>
            {
                _ui.HideScreen<SignalBlockScreen>();
                _audioService.Play(Sounds.UiClickSound);
            };

            _ui.GetScreen<SignalBlockScreen>().ChangeTabButtonClick += (index) =>
            {
                _data.RuntimeData.TabType = (SignalBlockType)(index + 1);
                _ui.GetScreen<SignalBlockScreen>().SignalBlockPanels.InitItems(_data.StaticData.BlocksByType[_data.RuntimeData.TabType].Blocks,
                    _data.SaveData.Currency,
                    _data.SaveData.IsBlockResearched[_data.RuntimeData.TabType], false);
                _audioService.Play(Sounds.UiClickSound);
                _ui.GetScreen<SignalBlockScreen>()
                    .UpdateDescriptionText(_data.StaticData.SignalBlockDataByType[_data.RuntimeData.TabType].BlockDescription);
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