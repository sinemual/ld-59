using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;

namespace Client.Data
{
    [Serializable]
    public class PrefabData
    {
        public GameObject EmptyPrefab;

        [Header("Prefabs - VFXs")] 
        public GameObject BloodVfxPrefab;
        
        [Header("Prefabs - UI")] 
        public GameObject DamageWorldUiPrefab;
        public GameObject WorldUiPrefab;
        public SerializedDictionary<ScreenType, BaseScreen> ScreenPrefabs;
        
        [Header("Pool Prefabs")] 
        public SerializedDictionary<GameObject, int> PoolPrefabData;
    }
}