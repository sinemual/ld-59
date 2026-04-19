using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Client.Data.Core;
using Client.Data.Equip;
using Client.DevTools.MyTools;
using Data;
using Data.Base;
using TriInspector;
using UnityEngine;

namespace Client.Data
{
    [CreateAssetMenu(menuName = "GameData/StaticData", fileName = "StaticData")]
    public class StaticData : BaseDataSO
    {
        [Header("Prefabs")] public PrefabData PrefabData;

        public int AlwaysLoadWorldId;
        public List<LevelData> Levels;
        public LevelData LevelLooped;
        public string ProjectSolutionEndText;
        public string SignalEndText;

        [Header("Tutorials & Tasks")]
        //public SerializedDictionary<TutorialStep, bool> TutorDependenceByStep;
        public List<TutorialData> TutorialData;

        public Dictionary<TutorialStep, TutorialData> TutorialDataByStep;

        [Header("Current Game Data")] public List<SignalBlockData> SignalBlockDatas;
        public SerializedDictionary<SignalBlockType, BlocksGroup> BlocksByType;
        public Dictionary<SignalBlockType, SignalBlockData> SignalBlockDataByType;

        [Header("Tags & Layers")] [Group("Tags")] [Tag]
        public string GroundTag;

        [Group("Layers")] public LayerMask RaycastMask;
        [Group("Layers")] public LayerMask PlayerMask;
        [Group("Layers")] public LayerMask BlocksMask;

        public int GetPlayerLayer => Utility.ToLayer(PlayerMask);
        public int GetBlocksLayer => Utility.ToLayer(BlocksMask);

        public int GetInvertedProjectileLayer()
        {
            int layerToIgnore = LayerMask.NameToLayer("Projectile");
            int layerMask = ~(1 << layerToIgnore);
            return layerMask;
        }

        public void InitComfortableData()
        {
            TutorialDataByStep = new Dictionary<TutorialStep, TutorialData>();
            foreach (var tutrData in TutorialData)
                TutorialDataByStep.Add(tutrData.TutorialStep, tutrData);

            SignalBlockDataByType = new Dictionary<SignalBlockType, SignalBlockData>();
            foreach (var blockData in SignalBlockDatas)
                SignalBlockDataByType.TryAdd(blockData.SignalBlockType, blockData);
        }

        public SignalBlockData GetBlockByParameters(SignalBlockType blockType, InputDirection inputDirection)
        {
            foreach (var signalBlockData in SignalBlockDatas)
            {
                if (signalBlockData.InputDirection == inputDirection && signalBlockData.SignalBlockType == blockType)
                    return signalBlockData;
            }

            return null;
        }
    }

    [Serializable]
    public class BlocksGroup
    {
        public List<SignalBlockData> Blocks;
    }
}