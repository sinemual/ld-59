using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class MonoEntitiesContainer : MonoBehaviour
{
    public List<MonoEntity> MonoEntities;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        GetAllChildren();
    }
#endif
    
    public void GetAllChildren()
    {
        MonoEntities = new List<MonoEntity>();
        MonoEntities.AddRange(GetComponentsInChildren<MonoEntity>(true));
        MonoEntities.RemoveAt(0);
    }
    
    public void DisposeMonoEntityChildren()
    {
        foreach (var child in MonoEntities)
        {
            if (child.Entity.IsAlive())
            {
                Destroy(child.Entity.Get<GameObjectProvider>().Value);
                child.Entity.Destroy();
            }
        }
    }
    
    public void ProvideMonoEntityChildren(EcsWorld world)
    {
        for (int i = 0; i < MonoEntities.Count; i++)
        {
            if(MonoEntities[i].transform.TryGetComponent(out PoolObject _))
                continue;
            EcsEntity entity = world.NewEntity();
            MonoEntities[i].Provide(ref entity);
        }
    }
}