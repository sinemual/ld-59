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

        //private EcsFilter<SignalBlockProvider, MyGrid> _blockFilter;
        private EcsFilter<SignalStartButtonTapEvent> _eventFilter;
        private EcsFilter<BlocksChain> _blocksChainFilter;
        private EcsFilter<LoneWolfTag, WorkState> _loneWolfFilter;
        private EcsFilter<OutputBlockMarker> _outputBlockFilter;
        
        public void Run()
        {
            if (_eventFilter.IsEmpty())
                return;
            
            foreach (var idx in _blocksChainFilter)
            {
                ref var chainEntity = ref _blocksChainFilter.GetEntity(idx);
                ref var chainData = ref chainEntity.Get<BlocksChain>().Value;

                for (int i = 0; i < chainData.Count; i++)
                {
                    ref var blockData = ref chainData[i].Get<SignalBlockDataComponent>().Value;
                    
                    if (chainData[i].Has<InputBlock>())
                    {
                        ref var inputBlockEntity = ref chainData[i].Get<InputBlock>().Value;
                        ref var inputBlockData = ref inputBlockEntity.Get<SignalBlockDataComponent>().Value;
                        
                        if (inputBlockData.SignalBlockType == SignalBlockType.OneSignal ||
                            inputBlockData.SignalBlockType == SignalBlockType.Multiplier)
                            chainData[i].Get<SignalValue>().Value += inputBlockEntity.Get<SignalValue>().Value;
                        
                        if (inputBlockData.SignalBlockType == SignalBlockType.OneSignal && 
                            blockData.SignalBlockType == SignalBlockType.Multiplier)
                            chainData[i].Get<SignalValue>().Value = inputBlockEntity.Get<SignalValue>().Value * 2;
                    }
                    
                    if (blockData.SignalBlockType == SignalBlockType.OneSignal)
                        chainData[i].Get<SignalValue>().Value += 1;
                }
                
                chainEntity.Destroy();
            }
            
            foreach (var idx in _loneWolfFilter)
            {
                ref var blockEntity = ref _loneWolfFilter.GetEntity(idx);

                foreach (var idz in _outputBlockFilter)
                {
                    ref var outputBlockEntity = ref _outputBlockFilter.GetEntity(idz);

                    outputBlockEntity.Get<SignalValue>().Value *= 3;
                }
            }
        }
    }
}