using System;
using UnityEngine;

[Serializable]
public struct SignalStartButtonProvider
{
    public ParticleSystem TapVfx;
    public ParticleSystem RadarVfx;
    
    public GameObject ButtonTransform;
    public GameObject ButtonGlow;
    public GameObject ButtonDisabled;
}