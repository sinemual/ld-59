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
                    if (chainData[i].Has<InputBlock>())
                    {
                        ref var inputBlockEntity = ref chainData[i].Get<InputBlock>().Value;
                        ref var inputBlockData = ref inputBlockEntity.Get<SignalBlockDataComponent>().Value;
                        if (inputBlockData.SignalBlockType == SignalBlockType.OneSignal)
                            chainData[i].Get<SignalValue>().Value += inputBlockEntity.Get<SignalValue>().Value;
                    }
                    ref var blockData = ref chainData[i].Get<SignalBlockDataComponent>().Value;
                    if (blockData.SignalBlockType == SignalBlockType.OneSignal)
                        chainData[i].Get<SignalValue>().Value += 1;
                }
                
                chainEntity.Destroy();
            }
        }
    }
}