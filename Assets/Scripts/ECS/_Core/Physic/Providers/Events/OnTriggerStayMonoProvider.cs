using System;
using Leopotam.Ecs;
using UnityEngine;

public class OnTriggerStayMonoProvider : MonoPhysicsProviderBase
{
    private void OnTriggerStay(Collider col)
    {
        if (!_entity.IsAlive()) return;

        _entity.Get<OnTriggerStayEvent>() = new OnTriggerStayEvent
        {
            Collider = col,
            Sender = gameObject
        };
    }
}