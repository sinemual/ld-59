using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Splines;

[Serializable]
public struct LevelProvider
{
    public Transform HeroSpawnPoint;
    public Transform LadleSpawnPoint;
    public Transform WorkerSpawnPoint;
    public Transform ShopItemSpawnPoint;
    public Transform[] ClientSpawnPoints;
}