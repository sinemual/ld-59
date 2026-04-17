using System;
using System.Collections.Generic;
using Client.Data.Core;
using EPOOutline;
using UnityEngine;

[Serializable]
public struct CharacterProvider
{
    public GameObject View;
    public Transform Hands;
    public Collider Collider;
    public Collider DeathCollider;
    public Outlinable Outlinable;
}