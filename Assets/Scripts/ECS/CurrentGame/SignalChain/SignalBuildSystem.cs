using Client.Data;
using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class SignalBuildSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<SignalBlockProvider, MyGrid> _blockFilter;
        private EcsFilter<SignalBlockProvider, LoneWolfTag> _loneWolfFilter;
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

                var distance = _data.BalanceData.DistanceBetweenBlocks;
                blockEntity.Del<OutputBlockMarker>();
                Transform signalInputTransform = _signalInputFilter.Get2(0).Value.transform;
                if (Vector3.Distance(blockGo.transform.position, signalInputTransform.position) <= distance &&
                    BlockIsLookingToAnotherBlock(signalInputTransform, blockGo.transform))
                    blockEntity.Get<OutputBlockMarker>();

                blockEntity.Del<SignalValue>();
                blockEntity.Del<InputBlock>();

                foreach (var idz in _blockFilter)
                {
                    ref var difBlockEntity = ref _blockFilter.GetEntity(idz);
                    ref var difSignalBlockData = ref difBlockEntity.Get<SignalBlockDataComponent>().Value;
                    ref var difBlockGo = ref difBlockEntity.Get<GameObjectProvider>().Value;

                    if (difBlockGo == blockGo)
                        continue;

                    if (blockEntity.Has<LoneWolfTag>())
                        blockEntity.Get<WorkState>();

                    if (Vector3.Distance(blockGo.transform.position, difBlockGo.transform.position) <= distance)
                    {
                        //Debug.Log($"{blockGo} -> {difBlockGo} = {BlockIsLookingToAnotherBlock(blockGo.transform, difBlockGo.transform)} | {IsInputDirectionTrue(blockGo.transform, difBlockGo.transform, signalBlockData.InputDirection)}");
                        if (BlockIsLookingToAnotherBlock(blockGo.transform, difBlockGo.transform) &&
                            IsInputDirectionTrue(blockGo.transform, difBlockGo.transform, signalBlockData.InputDirection))
                        {
                            //Debug.Log($"{blockGo} -> {difBlockGo} - YES");
                            blockEntity.Get<InputBlock>().Value = difBlockEntity;
                        }
                        
                        if (blockEntity.Has<LoneWolfTag>())
                            blockEntity.Del<WorkState>();
                    }
                }
            }
        }
        
        

        private bool BlockIsLookingToAnotherBlock(Transform targetBlock, Transform otherBlock)
        {
            Vector3 dirToTarget = (targetBlock.transform.position - otherBlock.transform.position).normalized;
            float dot = Vector3.Dot(otherBlock.transform.forward, dirToTarget);
            bool isLooking = dot > 0.95f;

            return isLooking;
        }

        private bool IsInputDirectionTrue(Transform targetBlock, Transform otherBlock, InputDirection inputDirection)
        {
            Vector3 localPos = targetBlock.InverseTransformPoint(otherBlock.position);
            if (Mathf.Abs(localPos.x) > Mathf.Abs(localPos.z))
            {
                if (localPos.x > 0f)
                    return inputDirection == InputDirection.Right;
                return inputDirection == InputDirection.Left;
            }

            return localPos.z < 0f && inputDirection == InputDirection.Back;
        }
    }

    public struct WorkState : IEcsIgnoreInFilter
    {
    }
}