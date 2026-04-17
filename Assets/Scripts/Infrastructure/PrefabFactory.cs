using System;
using Client.ECS.CurrentGame.Level;
using Leopotam.Ecs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client.Factories
{
    public class PrefabFactory
    {
        private readonly EcsWorld _world;
        private readonly PoolService _poolService;
        private readonly CleanService _cleanService;

        private Transform _defaultParent;

        public PrefabFactory(EcsWorld world, PoolService poolService, CleanService cleanService)
        {
            _world = world;
            _poolService = poolService;
            _cleanService = cleanService;
        }

        public void SetDefaultParent(Transform parent)
        {
            _defaultParent = parent;
            _cleanService.SetDefaultParent(parent);
        }

        public EcsEntity Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null, bool isFromThisLevel = true)
        {
            EcsEntity entity = _world.NewEntity();
            if (isFromThisLevel)
                entity.Get<FromThisLevelMarker>();

            /*if (parent == null)
                parent = _defaultParent;
            else
                parent.SetParent(_defaultParent);*/

            GameObject go = _poolService.IsPrefabHavePool(prefab) ? _poolService.GetGameObjectFromPool(prefab) : Object.Instantiate(prefab);
            go.SetActive(false);
            go.transform.position = position;
            go.transform.rotation = rotation;
            go.transform.SetParent(parent);
            go.transform.localScale = prefab.transform.lossyScale;
            go.SetActive(true);
            if (go.TryGetComponent(out MonoEntity monoEntity))
                monoEntity.Provide(ref entity);

            if (go.TryGetComponent(out MonoEntitiesContainer monoEntityContainer))
                monoEntityContainer.ProvideMonoEntityChildren(_world);

            return entity;
        }

        public GameObject SpawnGo(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (parent == null)
                parent = _defaultParent;

            GameObject go = _poolService.IsPrefabHavePool(prefab) ? _poolService.GetGameObjectFromPool(prefab) : Object.Instantiate(prefab);

            go.transform.position = position;
            go.transform.rotation = rotation;
            go.transform.SetParent(parent);
            go.transform.localScale = prefab.transform.lossyScale;
            return go;
        }

        public void SpawnWithEntity(ref EcsEntity entity, GameObject prefab, Vector3 position = default, Quaternion rotation = default,
            Transform parent = null)
        {
            entity.Get<FromThisLevelMarker>();

            if (parent == null)
                parent = _defaultParent;

            GameObject go = _poolService.IsPrefabHavePool(prefab) ? _poolService.GetGameObjectFromPool(prefab) : Object.Instantiate(prefab);

            go.transform.position = position;
            go.transform.rotation = rotation;
            go.transform.parent = parent;

            if (go.TryGetComponent(out MonoEntity monoEntity))
                monoEntity.Provide(ref entity);
        }

        public void Despawn(ref EcsEntity entity)
        {
            if (entity.IsAlive())
            {
                if (entity.Has<GameObjectProvider>())
                {
                    ref var entityGo = ref entity.Get<GameObjectProvider>().Value;

                    if (_poolService.IsGameObjectHavePool(entityGo))
                        _cleanService.DespawnGameObject(entityGo);
                    else
                        _cleanService.DestroyGameObject(entityGo);
                }
                else
                    entity.Destroy();
            }
        }

        public void Despawn(GameObject go)
        {
            //Debug.Log($"go {go.name}", go);
            if (_poolService.IsGameObjectHavePool(go))
                _cleanService.DespawnGameObject(go);
            else
                _cleanService.DestroyGameObject(go);
        }
    }
}