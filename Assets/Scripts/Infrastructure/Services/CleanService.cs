using Client;
using Client.DevTools.MyTools;
using Leopotam.Ecs;
using UnityEngine;

public class CleanService
{
    private PoolService _poolService;

    private Transform _defaultParent;

    public CleanService(PoolService poolService)
    {
        _poolService = poolService;
    }

    public void SetDefaultParent(Transform parent) => _defaultParent = parent;

    public void DespawnGameObject(GameObject go)
    {
        PrepareEcsEntity(go);
        if (_poolService.IsGameObjectHavePool(go))
        {
            _poolService.PutGameObjectToPool(go);
            go.SetActive(false);
        }
    }

    public void DestroyGameObject(GameObject go)
    {
        PrepareEcsEntity(go);
        Object.Destroy(go);
    }

    private void PrepareEcsEntity(GameObject go)
    {
        if (go && go.TryGetComponent(out MonoEntity monoEntity))
        {
            ref var entity = ref monoEntity.Entity;

            if (!entity.IsAlive())
                return;

            if (entity.Has<RigidbodyProvider>())
                Utility.ResetRigibodyVelocity(entity.Get<RigidbodyProvider>().Value);

            if (entity.Has<AnimatorProvider>())
                Utility.Animation.ResetAnimator(entity.Get<AnimatorProvider>().Value);

            if (go.TryGetComponent(out MonoEntitiesContainer monoEntitiesContainer))
            {
                if (IsDefaultParent(go.transform))
                    monoEntitiesContainer.GetAllChildren();

                monoEntitiesContainer.DisposeMonoEntityChildren();
            }
            entity.Destroy();
        }
    }

    private bool IsDefaultParent(Transform tr) => tr == _defaultParent;
}