using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;

namespace Client
{
    public class SignalCalculateSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<SignalBlockProvider, MyGrid> _blockFilter;
        private EcsFilter<SignalStartButtonTapEvent> _eventFilter;
        
        public void Run()
        {
            if (_eventFilter.IsEmpty())
                return;
            
            foreach (var idx in _blockFilter)
            {
                ref var blockEntity = ref _blockFilter.GetEntity(idx);
                ref var blockData = ref blockEntity.Get<SignalBlockDataComponent>().Value;

                if (blockEntity.Has<InputBlockType>())
                {
                    ref var input = ref blockEntity.Get<InputBlockType>().Value;
                    if (input == SignalBlockType.OneSignal)
                        blockEntity.Get<SignalValue>().Value += 1;
                }

                if (blockData.SignalBlockType == SignalBlockType.OneSignal)
                    blockEntity.Get<SignalValue>().Value += 1;
            }
        }
    }
}