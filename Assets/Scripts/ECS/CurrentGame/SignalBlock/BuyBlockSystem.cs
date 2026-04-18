using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class BuyBlockSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<BuyBlockRequest> _reqFilter;
        private EcsFilter<ManipulatorProvider> _manipulatorFilter;

        public void Run()
        {
            foreach (var idx in _reqFilter)
            {
                ref var requestEntity = ref _reqFilter.GetEntity(idx);
                ref var request = ref requestEntity.Get<BuyBlockRequest>();

                foreach (var idz in _manipulatorFilter)
                {
                    ref var manipulatorEntity = ref _manipulatorFilter.GetEntity(idz);
                    ref var manipulator = ref manipulatorEntity.Get<ManipulatorProvider>();
                    ref var manipulatorGo = ref manipulatorEntity.Get<GameObjectProvider>().Value;

                    var blockData = _data.StaticData.GetBlockByParameters(request.SignalBlockType, request.InputDirection);
                    if (!manipulatorEntity.Has<IsHaveBlockState>())
                    {
                        EcsEntity blockEntity = _prefabFactory.Spawn(blockData.Prefab, manipulatorGo.transform.position, Quaternion.identity,
                            manipulatorGo.transform);
                        manipulatorEntity.Get<IsHaveBlockState>().BlockEntity = blockEntity;
                        blockEntity.Get<SignalBlockDataComponent>().Value = blockData;
                        var blockGo = blockEntity.Get<GameObjectProvider>().Value;
                        blockGo.transform.SetParent(manipulatorGo.transform);
                        blockGo.transform.localPosition = Vector3.zero;
                        //PrimeTween.Tween.LocalPosition(blockGo.transform, Vector3.up, 0.5f);
                    }
                    else
                    {
                        _world.NewEntity().Get<AddCurrencyRequest>().Value = blockData.BuyPrice;
                    }
                }

                requestEntity.Del<BuyBlockRequest>();
            }
        }
    }
}