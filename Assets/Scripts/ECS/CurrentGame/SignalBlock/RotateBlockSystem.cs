using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using PrimeTween;
using UnityEngine;

namespace Client
{
    public class RotateBlockSystem : IEcsRunSystem
    {
        private SharedData _data;
        private EcsWorld _world;
        private PrefabFactory _prefabFactory;

        private EcsFilter<ManipulatorProvider, IsHaveBlockState, InGridState> _manipulatorFilter;
        private EcsFilter<GridCell> _gridFilter;

        public void Run()
        {
            if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.1f)
            {
                foreach (var idz in _manipulatorFilter)
                {
                    ref var manipulatorEntity = ref _manipulatorFilter.GetEntity(idz);
                    ref var manipulator = ref manipulatorEntity.Get<ManipulatorProvider>();
                    ref var gridCellNum = ref manipulatorEntity.Get<InGridState>().GridCellNum;
                    ref var manipulatorGo = ref manipulatorEntity.Get<GameObjectProvider>().Value;

                    ref var blockEntity = ref manipulatorEntity.Get<IsHaveBlockState>().BlockEntity;
                    ref var blockGo = ref blockEntity.Get<GameObjectProvider>().Value;
                    blockGo.transform.Rotate(Vector3.up, 90f * (Input.mouseScrollDelta.y > 0.0f ? 1.0f : -1.0f));
                    /*float angle = Input.mouseScrollDelta.y > 0f ? 90f : -90f;

                    Tween.LocalRotation(
                        blockGo.transform,
                        endValue: blockGo.transform.localEulerAngles + Vector3.up * angle,
                        duration: 0.15f
                    );*/
                }
            }
        }
    }
}