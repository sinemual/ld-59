using System.Collections.Generic;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class SignalChainSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<SignalBlockProvider, MyGrid, OutputBlockMarker> _blockFilter;
        private EcsFilter<SignalInputProvider, GameObjectProvider> _signalInputFilter;
        private EcsFilter<SignalStartButtonTapEvent> _eventFilter;

        public void Run()
        {
            if (_eventFilter.IsEmpty())
                return;

            foreach (var idx in _blockFilter)
            {
                ref var blockEntity = ref _blockFilter.GetEntity(idx);
                ref var signalBlockData = ref blockEntity.Get<SignalBlockDataComponent>().Value;
                ref var blockGo = ref blockEntity.Get<GameObjectProvider>().Value;

                EcsEntity chainEntity = _world.NewEntity();
                chainEntity.Get<BlocksChain>().Value = new List<EcsEntity>();
                chainEntity.Get<BlocksChain>().Value.Add(blockEntity);

                int stopCounter = 0;
                while (blockEntity.Has<InputBlock>())
                {
                    EcsEntity inputBlockEntity = blockEntity.Get<InputBlock>().Value;
                    chainEntity.Get<BlocksChain>().Value.Add(inputBlockEntity);
                    blockEntity = inputBlockEntity;
                    stopCounter += 1;
                    if (stopCounter > 100)
                    {
                        Debug.Log($"while break");
                        break;
                    }
                }
                
                chainEntity.Get<BlocksChain>().Value.Reverse();
            }
        }
    }
}