using System;
using Leopotam.Ecs;
using UnityEngine;

public class OnCollisionEnterMonoProvider : MonoPhysicsProviderBase
{
    /*private void OnCollisionEnter(Collision col)
    {
        Debug.Log($"OnCollisionEnterMonoProvider {col}");
        if (!_entity.IsAlive()) return;
        _entity.Get<OnCollisionEnterEvent>() = new OnCollisionEnterEvent
        {
            Collision = col,
            Sender = _entity,
            CollisionName = gameObject.name
        };
        
        /*_entity.GetInternalWorld().NewEntity().Get<OnCollisionEnterEvent>() = new OnCollisionEnterEvent()
        {
            Collision = col,
            Sender = _entity,
            CollisionName = gameObject.name
        };#1#
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (!_entity.IsAlive()) return;
        _entity.Get<OnCollisionEnterEvent>() = new OnCollisionEnterEvent
        {
            Collision = collision,
            Sender = _entity,
            CollisionName = gameObject.name
        };
    }
}
