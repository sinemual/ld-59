using Client;
using Client.Data.Core;
using Leopotam.Ecs;
using PrimeTween;
using UnityEngine;

public class TweenAroundMovementSystem : IEcsRunSystem
{
    private SharedData _data;
    private EcsWorld _world;

    private EcsFilter<TweenAroundMovement>.Exclude<MovingState> _filter;

    public void Run()
    {
        foreach (var idx in _filter)
        {
            ref var entity = ref _filter.GetEntity(idx);
            ref var moveItemGo = ref entity.Get<GameObjectProvider>().Value;
            ref var target = ref entity.Get<TweenAroundMovement>().Target;

            var to = target.transform.position;

            Vector3 midOffset = Random.onUnitSphere;
            midOffset.y = Mathf.Abs(midOffset.y) + 1f;
            midOffset *= 1.5f;
            Vector3 mid = to + midOffset;
            entity.Get<MovingState>();
            moveItemGo.transform.localScale = Vector3.zero;
            var ecsEntity = entity;
            Sequence.Create()
                .Chain(Tween.Scale(moveItemGo.transform, Vector3.one, 0.15f))
                .Group(Tween.Position(moveItemGo.transform, mid, 0.15f))
                .Group(Tween.Rotation(moveItemGo.transform, Random.rotation, 0.15f))
                .Chain(Tween.Position(moveItemGo.transform, to, 0.15f))
                .Group(Tween.Scale(moveItemGo.transform, Vector3.zero, 0.15f))
                .OnComplete(() =>
                {
                    ecsEntity.Get<MovingCompleteEvent>();
                    ecsEntity.Del<TweenAroundMovement>();   
                    ecsEntity.Del<MovingState>();
                    //Debug.Log($"MovingCompleteEvent");
                });
        }
    }
}