using Leopotam.Ecs;
using UnityEngine;

public class OnCollisionStayMonoProvider : MonoPhysicsProviderBase
{
    private void OnCollisionStay(Collision col)
    {
        _entity.GetInternalWorld().NewEntity().Get<OnCollisionStayEvent>() = new OnCollisionStayEvent()
        {
            Collision = col,
            Sender = _entity,
            CollisionName = gameObject.name
        };
    }
}