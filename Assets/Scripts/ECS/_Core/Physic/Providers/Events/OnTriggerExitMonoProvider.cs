using System;
using Leopotam.Ecs;
using UnityEngine;

public class OnTriggerExitMonoProvider : MonoPhysicsProviderBase
{
    private void OnTriggerExit(Collider col)
    {
        if (!_entity.IsAlive()) return;

        _entity.Get<OnTriggerExitEvent>() = new OnTriggerExitEvent
        {
            Collider = col,
            Sender = gameObject
        };
    }
}