using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public struct OnCollisionEnterEvent
{
    public EcsEntity Sender;
    public Collision Collision;
    public string CollisionName;
}

public struct OnCollisionStayEvent
{
    public EcsEntity Sender;
    public Collision Collision;
    public string CollisionName;
}
