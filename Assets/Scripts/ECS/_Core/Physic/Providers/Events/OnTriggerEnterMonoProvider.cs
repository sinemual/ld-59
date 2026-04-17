using System;
using Leopotam.Ecs;
using UnityEngine;

public class OnTriggerEnterMonoProvider : MonoPhysicsProviderBase
{
    private void OnTriggerEnter(Collider col)
    {
        if (!_entity.IsAlive()) return;

        _entity.Get<OnTriggerEnterEvent>() = new OnTriggerEnterEvent
        {
            Collider = col,
            Sender = gameObject
        };
    }
}