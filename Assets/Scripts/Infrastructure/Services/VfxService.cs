using Client.Data.Core;
using Client.Factories;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Infrastructure.Services
{
    public class VfxService
    {
        private SharedData _data;
        private PrefabFactory _prefabFactory;

        public VfxService(SharedData data, PrefabFactory prefabFactory)
        {
            _data = data;
            _prefabFactory = prefabFactory;
        }

        public EcsEntity CreateVfx(GameObject go, Vector3 createPosition, Quaternion quaternion, Transform parent = null)
        {
            EcsEntity spawnEntity = _prefabFactory.Spawn(go, createPosition, quaternion, parent);
            spawnEntity.Get<DespawnAtTimerRequest>();
            spawnEntity.Get<Timer<DespawnTimer>>().Value = _data.BalanceData.DespawnVfxTime;
            return spawnEntity;
        }
        
        public EcsEntity CreateVfxWithoutDespawnTimer(GameObject go, Vector3 createPosition, Quaternion quaternion, Transform parent = null)
        {
            EcsEntity spawnEntity = _prefabFactory.Spawn(go, createPosition, quaternion, parent);
            return spawnEntity;
        }
    }
}